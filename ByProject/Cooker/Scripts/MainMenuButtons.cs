using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    private void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)FindFirstObjectByType<FadeInOut>().FadeIn();
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Lost()
    {
        SceneManager.LoadScene(2);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
