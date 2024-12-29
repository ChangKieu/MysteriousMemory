using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    public Puzzle[] Puzzles;
    public Transform[] targetPositions;
    public GameObject hint;
    public GameObject cutScene;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        ShufflePuzzlePieces();
    }
    private void ShufflePuzzlePieces()
    {
        Vector3[] initialPositions = new Vector3[Puzzles.Length];
        for (int i = 0; i < Puzzles.Length; i++)
        {
            initialPositions[i] = Puzzles[i].transform.position;
        }

        for (int i = Puzzles.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            Vector3 tempPosition = initialPositions[i];
            initialPositions[i] = initialPositions[randomIndex];
            initialPositions[randomIndex] = tempPosition;
        }

        for (int i = 0; i < Puzzles.Length; i++)
        {
            Puzzles[i].transform.position = initialPositions[i];
        }
    }

    public void CheckPuzzleCompletion()
    {
        bool allCorrect = true;

        foreach (Puzzle piece in Puzzles)
        {
            if (!piece.isCorrect)
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            cutScene.SetActive(true);
            hint.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
