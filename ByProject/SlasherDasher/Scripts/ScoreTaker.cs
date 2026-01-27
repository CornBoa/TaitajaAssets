using TMPro;
using UnityEngine;

public class ScoreTaker : MonoBehaviour
{
    TextMeshProUGUI high;
    public bool isDead = false;
    void Start()
    {
        high = GetComponent<TextMeshProUGUI>();
        if (!isDead)
        {
            high.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
        }            
        else
        {
            ScoreSaver save = ScoreSaver.instance;
            save.SaveScore();
            if(save.currentScore > PlayerPrefs.GetInt("Player" + "_Score"))high.text = "New high score: " + PlayerPrefs.GetInt("Player" + "_Score").ToString();
            else high.text = "Score: " + save.currentScore.ToString();

        }
    }

}
