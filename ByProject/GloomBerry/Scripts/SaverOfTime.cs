using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaverOfTime : MonoBehaviour
{
    float timer;
    public static SaverOfTime instance;
    public TextMeshProUGUI timeText;   
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
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Update()
    {
        timer+= Time.deltaTime;
    }
   void OnSceneLoaded(Scene sc, LoadSceneMode load)
    {
        if (sc.buildIndex == 1)
        {
            timeText.gameObject.SetActive(false);
            timer = 0;
        }

        else if (sc.buildIndex == 2)
        {
            timeText.gameObject.SetActive(true);
            timeText.text = "Time survived: " + timer.ToString("F2") + "s";
        }
        else timeText.gameObject.SetActive(false);
    }
}
