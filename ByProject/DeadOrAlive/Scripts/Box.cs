using UnityEngine;
using UnityEngine.Windows.Speech;

public class Box : MonoBehaviour ,IDamagable
{
    DropMag drop;

    public void Died()
    {
        drop.DropEm();
        Destroy(gameObject);
    }

    public void TakeDMG(float dmg)
    {
        Debug.Log("Damage taken");
        Died();
    }

    void Start()
    {
        drop = GetComponent<DropMag>();
    }
}
