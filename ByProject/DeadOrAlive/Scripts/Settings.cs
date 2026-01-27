using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    static public Settings instance;
    public float sensetivity, masterVol, musicVol;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] List<AudioClip> testClipList;
    AudioSource audioSource;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void SoundCheck()
    {
        audioSource.PlayOneShot(testClipList[Random.Range(0,testClipList.Count)]);
    }
    public void UpdateMasterVol(float value)
    {
        if (value == 0)
        {
            audioMixer.SetFloat("MasterVolume", -80);
        }
        else audioMixer.SetFloat("MasterVolume", Mathf.Log10(value)* 20);
    }
    public void UpdateMusicVol(float value)
    {
        if (value == 0)
        {
            audioMixer.SetFloat("MusicVolume", -80);
        }
        else audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }
    public void UpdateSesitivity(float value)
    {
        sensetivity = value;
    }
}
