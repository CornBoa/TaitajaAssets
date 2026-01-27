using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    FadeInOut fadeInOut;
    private void Start()
    {
        fadeInOut = FindFirstObjectByType<FadeInOut>();
    }
    public void Play()
    {
        fadeInOut.onFadeOut.AddListener(delegate { LoadScene(1); });
        fadeInOut.FadeOut();
    }
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        fadeInOut.onFadeOut.AddListener(delegate { LoadScene(0); });
        fadeInOut.FadeOut();
    }
    public void Ded()
    {
        fadeInOut.onFadeOut.AddListener(delegate { LoadScene(2); });
        fadeInOut.FadeOut();
    }
}
