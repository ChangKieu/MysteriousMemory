using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneGame : MonoBehaviour
{
    public Button newGameButton;
    public Button continueButton;
    [SerializeField] GameObject sceneChanger;

    private void Start()
    {
        newGameButton.onClick.AddListener(StartNewGame);
        continueButton.onClick.AddListener(ContinueGame);
        Invoke("OffFadeIn", 1f);

    }
    void OffFadeIn()
    {
        sceneChanger.SetActive(false);
    }
    void StartNewGame()
    {
        GameProgressManager.Instance.ResetProgress();
        StartCoroutine(LoadRoom(1));  
    }

    void ContinueGame()
    {
        int progress = GameProgressManager.Instance.LoadProgress();
        StartCoroutine(LoadRoom(progress > 0 ? progress : 1)); 
    }
    public void LoadSceneGame(int romIndex)
    {
        StartCoroutine(LoadRoom(romIndex));
    }
    IEnumerator LoadRoom(int roomIndex)
    {
        sceneChanger.SetActive(true);
        sceneChanger.GetComponent<Animator>().SetTrigger("End");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("S" + roomIndex);  
    }
}
