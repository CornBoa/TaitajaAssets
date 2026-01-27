using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    void Start()
    {
        
    }
    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
