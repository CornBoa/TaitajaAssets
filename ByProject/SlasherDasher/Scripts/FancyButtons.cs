using UnityEngine;
using UnityEngine.UI;

public class FancyButtons : MonoBehaviour
{
    public AudioClip buttonClickSound;
    void Start()
    {
        foreach(Button brn in FindObjectsByType<Button>(FindObjectsInactive.Include,FindObjectsSortMode.None))
        {
            brn.gameObject.AddComponent<ButtonSelectVisual>();
            brn.onClick.AddListener( delegate { AudioManager.instance.PlayEffect(buttonClickSound); } );
        }
    }
}
