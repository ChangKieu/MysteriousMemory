using UnityEngine;
using UnityEngine.EventSystems;

public class Puzzle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 startPosition, originalPosition;
    private Transform startParent, originalParent;
    public bool isCorrect;
    [SerializeField] int id;
    public Transform[] targetPositions;
    [SerializeField] AudioClip sound;
    private void Start()
    {
        targetPositions = gameObject.GetComponentInParent<PuzzleManager>().targetPositions;
        startPosition = transform.position;
        originalPosition = transform.position;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = transform.position;
        startParent = transform.parent;     
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Transform closestTarget = null;
        float closestDistance = float.MaxValue;
        int index = 0, cur =0;
        foreach (Transform targetPosition in targetPositions)
        {
            float distance = Vector3.Distance(transform.position, targetPosition.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = targetPosition;
                cur = index;
            }
            index++;
        }

        if (closestDistance < 50f)  
        {
            Puzzle existingPiece = closestTarget.GetComponentInChildren<Puzzle>();

            if (existingPiece != null)
            {
                existingPiece.ReturnToStartPosition();
            }
            LeanTween.move(gameObject, closestTarget, 0.3f).setEase(LeanTweenType.easeInOutQuad);
            transform.SetParent(closestTarget);
            GetComponent<AudioSource>().PlayOneShot(sound);
            if(cur == id)
            {
                isCorrect = true;
            }
            else
            {
                isCorrect = false;
            }
        }
        else
        {
            LeanTween.move(gameObject, originalPosition, 0.3f).setEase(LeanTweenType.easeInOutQuad);
            transform.SetParent(originalParent);
        }

        PuzzleManager.Instance.CheckPuzzleCompletion(); 
    }
    public void ReturnToStartPosition()
    {
        LeanTween.move(gameObject, originalPosition, 0.3f).setEase(LeanTweenType.easeInOutQuad);
        transform.SetParent(originalParent);   
    }
}
