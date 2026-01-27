using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    Movement player;
    [SerializeField] Sprite tobe;
    [SerializeField] AudioClip spiked;
    bool primed = true;
    void Start()
    {
        player = FindFirstObjectByType(typeof(Movement)) as Movement;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (primed)
        {
            StopAllCoroutines();
            StartCoroutine(MegaDeath());
        }
            
    }
    IEnumerator MegaDeath()
    {
        yield return new WaitForSeconds(3);
        AudioManager.instance.PlaySound(spiked);
        GetComponent<SpriteRenderer>().sprite = tobe;
        player.TakeDmg(75);        
        primed = false;
    }
    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }
}
