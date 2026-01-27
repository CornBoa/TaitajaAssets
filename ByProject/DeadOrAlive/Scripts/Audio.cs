using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance;
    AudioSource source;
    private void Awake()
    {
        if(instance == null)instance = this;
    }
    private void Start()
    {
        source = GetComponent<AudioSource>();
    }
    public void PlayOne(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
