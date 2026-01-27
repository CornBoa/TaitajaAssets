using UnityEngine;

public class SantasLitlleHelper : MonoBehaviour
{
    AudioManager audioManager;
    void Start()
    {
        audioManager = AudioManager.instance;
    }
    public void PlayEffect(AudioClip clip)
    {
        if (clip != null) audioManager.PlayEffect(clip);
    }
    public void MusicChange(AudioClip clip)
    {
       if(clip != null) audioManager.PlayMusic(clip);
    }
}
