using UnityEngine;
using UnityEngine.Events;

public class Pause : MonoBehaviour
{
    [SerializeField] UnityEvent pausing,unpausing;
    bool paused = false;
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            pausing.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused=false;
            unpausing.Invoke();
        }
    }
}
