using UnityEngine;

public class CutSceneLoadScene : MonoBehaviour
{
    [SerializeField] float timeLeft;
    [SerializeField] GameManager gameManager;
    private void OnEnable()
    {
        Invoke("LoadSceneMenu", timeLeft);   
    }
    void LoadSceneMenu()
    {
        gameManager.BackToHome();
    }
}
