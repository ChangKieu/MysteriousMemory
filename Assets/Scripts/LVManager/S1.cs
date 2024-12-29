using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S1 : MonoBehaviour
{
    [Header("--------------------Raycast--------------------")]
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("--------------------UI--------------------")]
    [SerializeField] private Text notificationText;


    [Header("--------------------Manager--------------------")]
    GameManager gameManager;
    [SerializeField] GameObject missions;
    private bool getKey;
    bool isOpenNoti;

    [Header("--------------------Item--------------------")]
    [SerializeField] GameObject safeItem;
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    void Update()
    {
        if (!gameManager.CheckCompleted())
        {
            if (CheckCompletedMissions())
            {
                gameManager.SetCompleted(true);
            }
        }
        if (highlight != null)
        {
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) 
        {
            highlight = raycastHit.transform;
            if (highlight.CompareTag("Selectable") && highlight != selection)
            {
                if (highlight.gameObject.GetComponent<Outline>() != null)
                {
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlight = null;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject.name == "Door")
                {
                    if (!getKey)
                    {
                        string content = "Hãy nhìn dưới lớp phản chiếu, sự thật không chỉ nằm trên bề mặt.";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }
                    else if (!CheckCompletedMissions())
                    {
                        string content = "Xin hãy hoàn thành nhiệm vụ!";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }
                    else
                    {
                        gameManager.SetWin(true);
                    }
                }
                if (clickedObject.name == "Key")
                {
                    Destroy(clickedObject);
                    getKey = true;
                }
                if (clickedObject.name == "Table")
                {
                    if (safeItem != null)
                    {
                        safeItem.gameObject.SetActive(true);
                    }
                    else
                    {
                        string content = "Bí mật ở đây đã được khám phá.";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }
                }
            }
        }
    }
    bool CheckCompletedMissions()
    {
        for(int i=1; i < missions.transform.childCount; i++)
        {
            var mission = missions.transform.GetChild(i);
            if(!mission.transform.GetChild(0).gameObject.activeSelf)
                return false;
        }
        return true;
    }
    IEnumerator ShowNotification(string content)
    {
        isOpenNoti = true;
        notificationText.gameObject.SetActive(true);
        notificationText.text = content;
        notificationText.rectTransform.localScale = Vector3.zero;

        LeanTween.scale(notificationText.rectTransform, Vector3.one, 0.2f)
                         .setEase(LeanTweenType.easeInBack);
        yield return new WaitForSeconds(1.5f);
        notificationText.gameObject.SetActive(false);
        isOpenNoti = false;

    }
}
