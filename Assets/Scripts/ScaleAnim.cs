using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnim : MonoBehaviour
{
    [SerializeField] Vector3 ScaleTo;
    [SerializeField] float AnimateTime = 0.3f;
    [SerializeField] float Delay = 0f;
    public LeanTweenType EaseType = LeanTweenType.easeOutBack;
    public bool isDisplay = false;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        LeanTween.scale(gameObject,
            ScaleTo,
            AnimateTime)
            .setDelay(Delay)
            .setEase(EaseType);
    }
    private void OnDisable()
    {

        LeanTween.scale(gameObject,
        new Vector3(0, 0, 0),
        AnimateTime)
        .setDelay(Delay)
        .setEase(EaseType);


    }

}
