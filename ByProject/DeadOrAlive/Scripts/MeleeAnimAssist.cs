using UnityEngine;

public class MeleeAnimAssist : MonoBehaviour
{
    Melee melee;
    private void Start()
    {
        melee = GetComponentInParent<Melee>();   
    }
    public void onEvent()
    {
        melee.onEvent();
    }
}
