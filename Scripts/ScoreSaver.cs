using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreSaver : MonoBehaviour
{
    public static ScoreSaver instance;
    public int currentScore = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject scoreDisplay = GameObject.Find("ScoreDisplay");
        Debug.Log("Scene Loaded: " + scene.name);
        if (scoreDisplay != null)
        {
            scoreDisplay.GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore.ToString();
        }
    }
    public void AddScore(int score)
    {
        currentScore += score;
        GameObject.Find("ScoreDisplay").GetComponent<TextMeshProUGUI>().text = "Score: " + currentScore.ToString();
    }
    [ContextMenu("Save Score")]
    public void SaveScore()
    {
        if(PlayerPrefs.GetInt("HighScore") < currentScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
        }       
        PlayerPrefs.Save();
    }
    [ContextMenu("Show Score")]
    void Show()
    {
        Debug.Log("Current Score: " + PlayerPrefs.GetInt("Player" + "_Score"));
    }
    [ContextMenu("Reset Score")]
    void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
}
