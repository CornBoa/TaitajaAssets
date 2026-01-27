using UnityEngine;

public class DropMag : MonoBehaviour
{
    [SerializeField] GameObject resourceToDrop,medKit;
    BoxCollider2D toodee;
    private void Start()
    {
        toodee = GetComponent<BoxCollider2D>();
    }
    public void DropEm()
    {
        int i = Random.Range(1, 4);
        while (i > 0)
        {
            i--;
            Resource res = Instantiate(resourceToDrop).GetComponent<Resource>();
            if (toodee != null) res.SetRandomPos(toodee.bounds);
            else res.SetRandomPos(GetComponent<BoxCollider>().bounds);
        }
        Resource med = Instantiate(medKit).GetComponent<Resource>();
        if (toodee != null) med.SetRandomPos(toodee.bounds);
        else med.SetRandomPos(GetComponent<BoxCollider>().bounds);
    }
}
