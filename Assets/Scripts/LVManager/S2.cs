using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class S2 : MonoBehaviour
{
    [Header("--------------------Raycast--------------------")]
    private Transform highlight;
    private Transform selection;
    private RaycastHit raycastHit;

    [Header("--------------------UI--------------------")]
    [SerializeField] private Text notificationText;
    [SerializeField] GameObject txtTimeFind;
    [SerializeField] Text txtTimeLeft;
    [SerializeField] float timeFind;


    [Header("--------------------Manager--------------------")]
    GameManager gameManager;
    [SerializeField] GameObject missions;
    [SerializeField] GameObject listIngredient;
    private bool getKey, getRecipe;
    bool isOpenNoti;
    private bool isComplete;

    [Header("--------------------Item--------------------")]
    [SerializeField] GameObject safeItem;
    [SerializeField] GameObject findIngredient;
    [SerializeField] GameObject key;


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
                        string content = "Chỉ khi hương vị hòa quyện trọn vẹn, cánh cửa sẽ tự khắc mở ra.";
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
                if (clickedObject.name == "Recipe")
                {
                    getRecipe = true;
                }
                if (clickedObject.name == "Bookshelf")
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
                if(clickedObject.name == "Fireplace")
                {
                    if (!getRecipe)
                    {
                        string content = "Hãy tìm kiếm công thức nấu ăn.";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }
                    else if (isComplete)
                    {
                        string content = "Bí mật ở đây đã được khám phá.";
                        if (!isOpenNoti)
                        {
                            StartCoroutine(ShowNotification(content));
                        }
                    }
                    else
                    {
                        findIngredient.SetActive(true);
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
    public void BtnStart()
    {
        StartCoroutine(StartCountdown());
    }
    IEnumerator StartCountdown()
    {
        float remainingTime = timeFind;

        while (remainingTime > 0)
        {
            if (isComplete)
            {
                missions.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                key.SetActive(true);
                break;
            }
            if(listIngredient.transform.childCount == 0)
            {
                isComplete = true;
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            txtTimeLeft.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            txtTimeFind.transform.GetChild(0).GetComponent<Text>().text = string.Format("{0:00}:{1:00}", minutes, seconds);

            if (remainingTime < 30f)
            {
                txtTimeLeft.color = Color.red;
                txtTimeFind.transform.GetChild(0).GetComponent<Text>().color = Color.red;
            }
            else
            {
                txtTimeLeft.color = Color.white;
                txtTimeFind.transform.GetChild(0).GetComponent<Text>().color = Color.white;
            }
            remainingTime -= Time.deltaTime;

            yield return null;
        }

        txtTimeLeft.text = "00:00";
        TimeUp();
    }

    void TimeUp()
    {
        if (!isComplete)
        {
            findIngredient.SetActive(false);
            gameManager.SetLose(true);
        }
    }
}
