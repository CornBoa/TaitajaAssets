using System.Collections;
using UnityEngine;

public class ConferenceDoor : MonoBehaviour
{
    public bool IsOpen;
    public FadeInOut fadeInOut;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player") && IsOpen)
        {
            fadeInOut.FadeOut();
            IsOpen = false;
            StartCoroutine(WaitAndLoad());
            FindAnyObjectByType<PlayerMovement>().enabled = false;
        }
        
    }
    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(1f);
        fadeInOut.FadeIn();
        FindAnyObjectByType<PlayerMovement>().enabled = true;
        ResourcesGa.instance.Study(Random.Range(0,75));
    }
}
