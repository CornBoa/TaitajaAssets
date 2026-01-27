using System.Collections;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    [SerializeField] float dmgInterval, deathTime, DMG;
    Movement player;
    bool playerIn = false;
    void Start()
    {
        player = FindFirstObjectByType<Movement>();
        StartCoroutine(Death());
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
    void DealDmg()
    {
        if (playerIn)
        {
            player.TakeDmg(DMG);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIn = false;
        }
    }
}
