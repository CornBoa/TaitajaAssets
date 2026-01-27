#if UNITY_EDITOR
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Events;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;
public class BomboclatMenu : MonoBehaviour
{
    public Sprite buttonSprite, backgroundSprite, logoSprite;
    GameObject canvasGO, mainMenu,optionsMenu;
    public Font font;
    public string buttonText;
    [ContextMenu("Make Menu")]
    // This script stinks so I have to comment this myself
    void MakeMenu()
    {
        //This here makes a cnavas and sets it up
        Canvas canvas = FindFirstObjectByType<Canvas>(FindObjectsInactive.Include);
        if (canvas == null)
        {
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            canvasGO.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        }
        canvasGO = canvas.gameObject;
        canvasGO.AddComponent<MainMenuButtons>();
        canvasGO.AddComponent<FancyButtons>();

        //Then menu objects
        mainMenu = new GameObject("MainMenu");
        mainMenu.transform.SetParent(canvasGO.transform, false);
        optionsMenu = new GameObject("OptionsMenu");
        optionsMenu.transform.SetParent(canvasGO.transform, false);
        optionsMenu.SetActive(false);
        if (FindFirstObjectByType<EventSystem>(FindObjectsInactive.Include) == null)
        {
            GameObject es = new GameObject("EventSystem");
            es.AddComponent<EventSystem>();
            es.AddComponent<InputSystemUIInputModule>();
        }
        //Le Buttons
        for (int i = 0; i < 3;i++)
        {
            if(i == 1) buttonText = "Options";
            else if (i == 2) buttonText = "Exit";
            else buttonText = "Play";
            Vector2 pos = new Vector2(0, (i * -40));
            MakeButton(pos, mainMenu.transform, i);
        }
        buttonText = "Back";
        MakeButton(new Vector2(0,-40), optionsMenu.transform, 3);
        MakeSLider(Vector2.zero, optionsMenu.transform, 0);
        MakeSLider(new Vector2(0,40), optionsMenu.transform, 1);
        //Le Fade
        MakeTheFadePanel();
        if(FindFirstObjectByType<AudioManager>() == null)
        {
            GameObject audioManager = new GameObject("AudioManager");
            audioManager.AddComponent<AudioManager>();
            AudioMixer mixer = AssetDatabase.LoadAssetAtPath<AudioMixer>("Assets/Audio/AudioMix.mixer");
            audioManager.GetComponent<AudioManager>().mixer = mixer;
        }
    }
    void MakeTheFadePanel()
    {
        GameObject fadePanel = new GameObject("FadePanel");
        fadePanel.transform.SetParent(canvasGO.transform, false);
        fadePanel.AddComponent<RectTransform>().sizeDelta = new Vector2(1920, 1080);
        Image fadeImage = fadePanel.AddComponent<Image>();
        fadeImage.color = Color.black;
        FadeInOut fader = fadePanel.AddComponent<FadeInOut>();
        fader.fadeInOnStart = true;
        fader.fadingTime = 0.5f;
    }
    public void MakeButton(Vector2 pos, Transform parent,int i)
    {
        GameObject buttonGO = new GameObject("Button");
        buttonGO.transform.SetParent(parent, false);

        Image image = buttonGO.AddComponent<Image>();
        image.color = Color.white;

        Button button = buttonGO.AddComponent<Button>();

        RectTransform rect = buttonGO.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(160, 30);
        rect.anchoredPosition = pos;

        GameObject textGO = new GameObject("Text");
        textGO.transform.SetParent(buttonGO.transform, false);

        Text text = textGO.AddComponent<Text>();
        text.text = buttonText;
        text.alignment = TextAnchor.MiddleCenter;
        text.color = Color.black;
        text.font = font;

        RectTransform textRect = textGO.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.offsetMin = Vector2.zero;
        textRect.offsetMax = Vector2.zero;

        switch(i)
        {
            case 0:

                UnityEventTools.AddPersistentListener(button.onClick, canvasGO.GetComponent<MainMenuButtons>().Play);
                break;
            case 1:

                    UnityEventTools.AddBoolPersistentListener(button.onClick, mainMenu.SetActive, false);
                    UnityEventTools.AddBoolPersistentListener(button.onClick, optionsMenu.SetActive, true);            
                break;
            case 2:
                UnityEventTools.AddPersistentListener(button.onClick, canvasGO.GetComponent<MainMenuButtons>().Quit);
                break;
            case 3:
                UnityEventTools.AddBoolPersistentListener(button.onClick, mainMenu.SetActive, true);
                UnityEventTools.AddBoolPersistentListener(button.onClick, optionsMenu.SetActive, false);
                break;

        }
    }
    void MakeSLider(Vector2 pos, Transform parent,int i)
    {
        // ----------------------------
        // Slider Root
        // ----------------------------
        GameObject sliderGO = new GameObject("Slider", typeof(Slider), typeof(RectTransform));
        sliderGO.transform.SetParent(parent, false);

        RectTransform sliderRect = sliderGO.GetComponent<RectTransform>();
        sliderRect.sizeDelta = new Vector2(200, 20);
        sliderRect.anchoredPosition = pos;

        Slider slider = sliderGO.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = 1;
        slider.value = 0.5f;

        // ----------------------------
        // Background
        // ----------------------------
        GameObject background = new GameObject("Background", typeof(Image));
        background.transform.SetParent(sliderGO.transform, false);

        Image bgImage = background.GetComponent<Image>();
        bgImage.type = Image.Type.Sliced;

        RectTransform bgRect = background.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        // ----------------------------
        // Fill Area
        // ----------------------------
        GameObject fillArea = new GameObject("Fill Area", typeof(RectTransform));
        fillArea.transform.SetParent(sliderGO.transform, false);

        RectTransform fillAreaRect = fillArea.GetComponent<RectTransform>();
        fillAreaRect.anchorMin = new Vector2(0, 0.25f);
        fillAreaRect.anchorMax = new Vector2(1, 0.75f);
        fillAreaRect.offsetMin = new Vector2(10, 0);
        fillAreaRect.offsetMax = new Vector2(-10, 0);

        // Fill
        GameObject fill = new GameObject("Fill", typeof(Image));
        fill.transform.SetParent(fillArea.transform, false);

        Image fillImage = fill.GetComponent<Image>();
        fillImage.type = Image.Type.Sliced;
        fillImage.color = new Color(0.3f, 0.8f, 0.3f);

        RectTransform fillRect = fill.GetComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        // ----------------------------
        // Handle Slide Area
        // ----------------------------
        GameObject handleArea = new GameObject("Handle Slide Area", typeof(RectTransform));
        handleArea.transform.SetParent(sliderGO.transform, false);

        RectTransform handleAreaRect = handleArea.GetComponent<RectTransform>();
        handleAreaRect.anchorMin = Vector2.zero;
        handleAreaRect.anchorMax = Vector2.one;
        handleAreaRect.offsetMin = Vector2.zero;
        handleAreaRect.offsetMax = Vector2.zero;

        // Handle
        GameObject handle = new GameObject("Handle", typeof(Image));
        handle.transform.SetParent(handleArea.transform, false);

        Image handleImage = handle.GetComponent<Image>();

        RectTransform handleRect = handle.GetComponent<RectTransform>();
        handleRect.sizeDelta = new Vector2(20, 20);

        // ----------------------------
        // Assign slider references
        // ----------------------------
        slider.targetGraphic = handleImage;
        slider.fillRect = fillRect;
        slider.handleRect = handleRect;
        VolumeSlider sliderish = sliderGO.AddComponent<VolumeSlider>();
        UnityEventTools.AddPersistentListener<float>(slider.onValueChanged, sliderish.OnValueChange);
        if(i == 1) sliderish.kind = VolumeSlider.SliderKind.Effect;
    }
}
#endif