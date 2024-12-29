using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S3 : MonoBehaviour
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
    [SerializeField] private bool getLetter;
    bool isOpenNoti;

    [Header("--------------------Item--------------------")]
    [SerializeField] GameObject game1, game2, game3, game4, letter;
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

                if (clickedObject.name == "Chair" && clickedObject.tag == "Selectable")
                {
                    if (game1 != null)
                    {
                        game1.gameObject.SetActive(true);
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
                if (clickedObject.name == "Bed" && clickedObject.tag == "Selectable")
                {
                    if (game2 != null)
                    {
                        game2.gameObject.SetActive(true);
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
                if (clickedObject.name == "Book_S3" && clickedObject.tag == "Selectable")
                {
                    if (game3 != null)
                    {
                        game3.gameObject.SetActive(true);
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
                if (clickedObject.name == "Guitar" && clickedObject.tag == "Selectable")
                {
                    if (game4 != null)
                    {
                        game4.gameObject.SetActive(true);
                    }
                    else
                    {
                        string content = "Bí mật ở đây đã được khám phá.";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }
                }if (clickedObject.name == "Gargoyle")
                {
                    if (!getLetter)
                    {
                        string content = "Hãy tìm kiếm đủ các mảnh thư.";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }

                    else
                    {
                        letter.SetActive(true);
                    }
                }
            }
        }
    }
    bool CheckCompletedMissions()
    {
        for (int i = 1; i < missions.transform.childCount; i++)
        {
            var mission = missions.transform.GetChild(i);
            if (!mission.transform.GetChild(0).gameObject.activeSelf)
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
    public void GetLetter()
    {
        getLetter = true;
    }
}
