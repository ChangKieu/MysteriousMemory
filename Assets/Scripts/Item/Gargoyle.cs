using UnityEngine;

public class Gargoyle : MonoBehaviour
{
    public float rotateAngle = 45f;  
    public float duration = 2f;      

    void Start()
    {
        rotateAngle += transform.rotation.eulerAngles.y;
        RotateLeft();
    }

    void RotateLeft()
    {
        LeanTween.rotateY(gameObject, -rotateAngle, duration).setOnComplete(RotateRight);
    }

    void RotateRight()
    {
        LeanTween.rotateY(gameObject, rotateAngle, duration).setOnComplete(RotateLeft);
    }
}
