using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public enum SliderKind
    {
        Music,Effect
    }
    Slider slider;
    [SerializeField] SliderKind kind;
    float value;

    void Start()
    {
        slider = GetComponent<Slider>();
        switch (kind)
        {
            case SliderKind.Music:
                AudioManager.instance.mixer.GetFloat("MusicVol", out value);
                break;
            case SliderKind.Effect:
                AudioManager.instance.mixer.GetFloat("EffectVol", out value);
                break;
        }
        slider.value = DBToNormalized(value); 
    }
    float DBToNormalized(float dB)
    {
        float minDB = -80f;
        float maxDB = 0f;

        dB = Mathf.Clamp(dB, minDB, maxDB);
        return (dB - minDB) / (maxDB - minDB);
    }
    public void OnValueChange()
    {
        Debug.Log("called");
        switch (kind)
        {
            case SliderKind.Music:
                AudioManager.instance.MusicVolumeChange(slider.value);
                break;
            case SliderKind.Effect:
                AudioManager.instance.EffectVolumeChange(slider.value);
                break;
        }
    }
}
