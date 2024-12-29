using UnityEngine;
using UnityEngine.SceneManagement;

public class GameProgressManager : MonoBehaviour
{
    private const string ProgressKey = "GameProgress";  

    public static GameProgressManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveProgress(int progress)
    {
        PlayerPrefs.SetInt(ProgressKey, progress);
        PlayerPrefs.Save();
    }

    public int LoadProgress()
    {
        return PlayerPrefs.GetInt(ProgressKey, 0); 
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt(ProgressKey, 0);
        PlayerPrefs.Save();
    }
}
