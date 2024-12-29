using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class S2_Find : MonoBehaviour
{
    [Header("--------------------Item--------------------")]
    [SerializeField] GameObject txtTimeFind;
    [SerializeField] GameObject listIngredient;

    [Header("--------------------Manager--------------------")]
    GameManager gameManager;
    int numberIngredient;
    public bool isComplete;

    [Header("--------------------UI--------------------")]
    [SerializeField] Button btnStart;
    [SerializeField] Text txtTimeLeft;
    [SerializeField] Text txtNumber;

    private void Start()
    {
        numberIngredient = listIngredient.transform.childCount;
        gameManager = FindAnyObjectByType<GameManager>();
        btnStart.onClick.AddListener(() => PressStartButton());
    }
    void PressStartButton()
    {
        txtTimeFind.SetActive(true);
        btnStart.gameObject.SetActive(false);
        txtTimeLeft.gameObject.SetActive(true);
        listIngredient.SetActive(true);
        txtNumber.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!isComplete)
        {
            int currentIngredientCount = listIngredient.transform.childCount; 
            txtNumber.text = $"{currentIngredientCount}/{numberIngredient}";
        }
    }
}
