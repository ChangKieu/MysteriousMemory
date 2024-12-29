using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Number : MonoBehaviour
{
    [Header("--------------------UI--------------------")]
    [SerializeField] Button[] numberBtn;
    [SerializeField] GameObject hint;
    private List<int> numbers;

    void Start()
    {
        GenerateRandomNumbers();
        DisplayNumbers();
    }

    void GenerateRandomNumbers()
    {
        numbers = new List<int>();

        int randomIndex = Random.Range(0, numberBtn.Length);  

        for (int i = 0; i < numberBtn.Length; i++)
        {
            if (i == randomIndex)
            {
                int num = Random.Range(1, 100); 
                while (num % 3 == 0)
                {
                    num = Random.Range(1, 100); 
                }
                numbers.Add(num);  
            }
            else
            {
                int num = Random.Range(1, 100);
                num = num / 3 * 3;
                while (numbers.Contains(num))
                {
                    num = Random.Range(1, 100);
                    num = num / 3 * 3;
                }
                numbers.Add(num); 
            }
        }
    }

    void DisplayNumbers()
    {
        for (int i = 0; i < numberBtn.Length; i++)
        {
            Text buttonText = numberBtn[i].GetComponentInChildren<Text>(); 
            buttonText.text = numbers[i].ToString();  
            int index = i;  

            numberBtn[i].onClick.AddListener(() => OnButtonClick(index));
        }
    }

    void OnButtonClick(int index)
    {
        if (numbers[index] % 3 != 0)
        {
            CorrectAnswer();
            StartCoroutine(True(index, Color.green));

        }
        else
        {
            WrongAnswer();
            StartCoroutine(Wrong(index, Color.red));

        }
    }

    void CorrectAnswer()
    {
        Debug.Log("Số chính xác!");
    }

    void WrongAnswer()
    {
        Debug.Log("Số sai!");
    }

    IEnumerator Wrong(int index, Color color)
    {
        LeanTween.color(numberBtn[index].gameObject.GetComponent<RectTransform>(), color, 0.5f);
        foreach (Button btn in numberBtn)
        {
            btn.interactable = false;  
        }
        yield return new WaitForSeconds(1f);
        LeanTween.color(numberBtn[index].gameObject.GetComponent<RectTransform>(), Color.white, 0.5f);
        foreach (Button btn in numberBtn)
        {
            btn.interactable = true;  
        }
    }
    IEnumerator True(int index, Color color)
    {
        LeanTween.color(numberBtn[index].gameObject.GetComponent<RectTransform>(), color, 0.5f);
        yield return new WaitForSeconds(1f);
        hint.SetActive(true);
        Destroy(gameObject);
    }
}
