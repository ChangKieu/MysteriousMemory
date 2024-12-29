using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class S1_Safe : MonoBehaviour
{
    [Header("--------------------Code--------------------")]
    [SerializeField] int[] correctCode; 
    private int[] inputCode;

    [Header("--------------------UI--------------------")]
    [SerializeField] Button[] numberBtn;
    [SerializeField] Button cfBtn;
    [SerializeField] GameObject item;

    void Start()
    {
        inputCode = new int[correctCode.Length];
        cfBtn.onClick.AddListener(() => Confirm());
        for (int i = 0; i < numberBtn.Length; i++)
        {
            int index = i;  
            numberBtn[i].onClick.AddListener(() => PressButton(index));
        }

        UpdateDisplay(); 
    }
    void Confirm()
    {
        if (CheckCode())
        {
            StartCoroutine(ChangeButtonColorsTrue());
        }
        else
        {
           StartCoroutine(ChangeButtonColorsWrong());
        }
    }
    void PressButton(int index)
    {
        inputCode[index] = (inputCode[index] + 1) % 10; 
        UpdateDisplay();

    }
    void UpdateDisplay()
    {
        for (int i = 0; i < numberBtn.Length; i++)
        {
            numberBtn[i].gameObject.transform.GetChild(0).GetComponent<Text>().text = inputCode[i].ToString(); 
        }
    }

    bool CheckCode()
    {
        for (int i = 0; i < correctCode.Length; i++)
        {
            if (inputCode[i] != correctCode[i])
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator ChangeButtonColorsTrue()
    {
        for (int i = 0; i < numberBtn.Length; i++)
        {
            LeanTween.color(numberBtn[i].gameObject.GetComponent<RectTransform>(), Color.green, 0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        if(item!=null)
            item.SetActive(true);
        Destroy(gameObject);
    }
    IEnumerator ChangeButtonColorsWrong()
    {
        for (int i = 0; i < numberBtn.Length; i++)
        {
            LeanTween.color(numberBtn[i].gameObject.GetComponent<RectTransform>(), Color.red, 0.5f);
        }
        for (int i = 0; i < inputCode.Length; i++)
        {
            inputCode[i] = 0;
        }
        yield return new WaitForSeconds(0.5f);
        UpdateDisplay();
        for (int i = 0; i < numberBtn.Length; i++)
        {
            LeanTween.color(numberBtn[i].gameObject.GetComponent<RectTransform>(), Color.white, 0.5f);
        }
    }
}
