using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    AudioSource musicSource, effectSource;
    public AudioMixer mixer;
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
        musicSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();
    }
    public void MusicVolumeChange(float value)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
    }
    public void EffectVolumeChange(float value)
    {
        mixer.SetFloat("EffectVol", Mathf.Log10(value) * 20);
    }
}
