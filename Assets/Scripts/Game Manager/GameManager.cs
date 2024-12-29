using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("--------------------TimerUI--------------------")]
    [SerializeField] private GameObject panelLose;
    [SerializeField] private Button againButton, menuButton, pauseMenuButton, pauseContinueButton;

    [Header("--------------------Check--------------------")]
    [SerializeField] private bool isEnd;
    [SerializeField] private bool isDead;
    [SerializeField] private bool isCompleted;
    [SerializeField] private int roomIndex;

    [Header("--------------------Scene--------------------")]
    [SerializeField] GameObject sceneChanger;

    [Header("--------------------Player--------------------")]
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform posPlayer;
    private void Awake()
    {
        if (playerPrefab != null && posPlayer != null)
        {
            Instantiate(playerPrefab, posPlayer.position, posPlayer.rotation);
        }
    }
    private void Start()
    {
        //GameProgressManager.Instance.SaveProgress(roomIndex);
        againButton.onClick.AddListener(() => StartCoroutine(ResetGame()));
        menuButton.onClick.AddListener(() => StartCoroutine(BackToMenu()));
        pauseMenuButton.onClick.AddListener(() => StartCoroutine(BackToMenu()));
        pauseContinueButton.onClick.AddListener(() => Resume());
        Invoke("OffFadeIn", 1f);
    }
    void OffFadeIn()
    {
        sceneChanger.SetActive(false);
    }
    private void Update()
    {
        if (isEnd && isCompleted)
        {
            StartCoroutine(Win());
        }
        if (isDead)
        {
            Lose();
        }

    }

    IEnumerator Win()
    {
        sceneChanger.SetActive(true);
        sceneChanger.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);
        int nextRoomIndex = roomIndex + 1;
        GameProgressManager.Instance.SaveProgress(nextRoomIndex); 
        SceneManager.LoadScene("S" + nextRoomIndex);  
    }

    void Lose()
    {
        panelLose.SetActive(true);
    }



    IEnumerator ResetGame()
    {
        sceneChanger.SetActive(true);
        sceneChanger.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;           
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }
    public void BackToHome()
    {
        StartCoroutine(BackToMenu());
    }
    IEnumerator BackToMenu()
    {
        sceneChanger.SetActive(true);
        sceneChanger.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }
    public void SetWin(bool win)
    {
        isEnd = win;
    }
    public void SetLose(bool lose)
    {
        isDead = lose;
    }
    public void SetCompleted(bool complete)
    {
        isCompleted = complete;
    }
    public bool CheckCompleted()
    {
        return isCompleted;
    }
}
