using UnityEngine;

public class CheckGameObjectOpen : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject nextObject;
    private void Awake()
    {
        player = FindAnyObjectByType<PlayerMovement>();

    }
    private void OnEnable()
    {
        player.isOpen = true;
    }
    private void OnDisable()
    {
        player.isOpen = false;
    }
    public void OffObject()
    {
        if (nextObject != null)
            nextObject.tag = "Selectable";

        Destroy(gameObject);
    }
}
