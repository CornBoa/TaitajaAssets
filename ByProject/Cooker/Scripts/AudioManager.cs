using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] AudioSource musicSource, effectSource;
    public AudioMixer mixer;
    [SerializeField]AudioClip musicClip;
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
        PlayMusic(musicClip);
    }
    public void PlayMusic(AudioClip clip)
    {
        musicSource.loop = true;
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void PlayEffect(AudioClip clip)
    {
        effectSource.clip = clip;
        effectSource.Play();
    }
    public void MusicVolumeChange(float value)
    {
        if (value == 0) mixer.SetFloat("MusicVol", -80);
        else mixer.SetFloat("MusicVol", Mathf.Log10(value) * 20);
    }
    public void EffectVolumeChange(float value)
    {
        if(value == 0)mixer.SetFloat("EffectVol", -80);
        else mixer.SetFloat("EffectVol", Mathf.Log10(value) * 20);
    }
}
