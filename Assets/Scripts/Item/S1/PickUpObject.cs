using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [Header("--------------------Item--------------------")]
    [SerializeField] GameObject itemOpen;

    [Header("--------------------UI--------------------")]
    [SerializeField] GameObject completeMission;

    [SerializeField] GameObject cutScene;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (clickedObject == gameObject)
                {
                    if(itemOpen != null)
                    {
                        itemOpen.SetActive(true);
                    }
                    if(completeMission != null)
                    {
                        completeMission.SetActive(true);
                    }
                    if(cutScene != null) 
                    {
                        cutScene.SetActive(true); 
                    }
                    Destroy(gameObject);
                }


            }
        }
    }
}
