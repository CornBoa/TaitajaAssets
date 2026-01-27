using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{
    public float currentXP, XPtoLevel;
    public int currentLevel;
    public static StatsManager instance;
    public Slider xpSlider;
    public TextMeshProUGUI xpText;
    public GameObject levelUpPanel;
    public Animator UIUP;
    public AudioClip music;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        xpSlider.maxValue = XPtoLevel;
        xpSlider.value = currentXP;
        AudioManager.instance.PlayMusic(music);
    }
    public void AddXP( float xp)
    {
        currentXP += xp;     
        if ( currentXP >= XPtoLevel)
        {
            currentXP -= XPtoLevel;
            xpSlider.maxValue = XPtoLevel;
            LevelUp();
        }
        xpSlider.value = currentXP;
        xpText.text = "XP: " + currentXP + " / " + XPtoLevel;
    }
    void LevelUp()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        Debug.Log("Level Up!");
        currentLevel++;
        currentXP = 0;
        XPtoLevel = XPtoLevel * 1.1f;
        xpText.text = "XP: " + currentXP + " / " + XPtoLevel;
        xpSlider.maxValue = XPtoLevel;
        StartCoroutine(UpUI());
    }
    IEnumerator UpUI()
    {
        yield return new WaitForSeconds(1f);   
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);
        UIUP.SetTrigger("LvlUP");
    }
}
