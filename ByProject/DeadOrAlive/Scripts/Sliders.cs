using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    [SerializeField]Settings settings;
    Slider slider;
    void Start()
    {
        settings = Settings.instance;
        slider = GetComponent<Slider>();
    }
    public void Sensitivity()
    {
        settings.UpdateSesitivity(slider.value);
    }
    public void Music()
    {
        settings.UpdateMusicVol(slider.value);
    }
    public void Master()
    {
        settings.UpdateMasterVol(slider.value);
    }
}
