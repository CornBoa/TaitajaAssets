
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    Animator animator;
    List<IDamagable> dmgbles = new List<IDamagable>();
    [SerializeField] float DMG;
    bool attacking = false;
    [SerializeField]AudioClip attackSound;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !attacking)
        {
            Attack();
        }
    }
    void Attack()
    {
        attacking = true;
        AudioManager.instance.PlaySound(attackSound);
        animator.SetBool("Swing", true);
    }
    public void onEvent()
    {
        attacking=false;
        animator.SetBool("Swing", false);
        if(dmgbles.Count > 0)foreach (var dmg in dmgbles)
        {
            if(dmg != null)dmg.TakeDMG(DMG);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Added");
        if (collision.CompareTag("Damagable"))
        {
            dmgbles.Add(collision.GetComponent<IDamagable>());         
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        dmgbles.Remove(other.GetComponent<IDamagable>());
    }
}
