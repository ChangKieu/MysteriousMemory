using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    [SerializeField] float timeLeft;
    private void OnEnable()
    {
        Destroy(gameObject, timeLeft);
    }
}
