using UnityEngine;

public class Observer : MonoBehaviour
{
    GameManager gameManager;
    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.SetLose(true);
        }
    }
}
