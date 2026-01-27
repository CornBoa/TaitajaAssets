using NUnit.Framework;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    public enum KindOfEnemy
    {
        Melee,
        Ranged
    }
    public KindOfEnemy kind;
    public int DMG, HP;
    public float attackCoolDown,attackRange = 5f;
    float timer;
    Transform player;
    public Berry berryPrefab;
    public GameObject projectile;
    bool move = true;
    public AudioClip deathSound;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if(timer < attackCoolDown)
        {
            timer += Time.deltaTime;
        }
        MoveTowardsPlayer();
        if(KindOfEnemy.Ranged == kind)
        {
            RangedBehave();
        }
        else
        {
            MeleeBehave();
        }
    }
    void MeleeBehave()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            if (timer > attackCoolDown)
            {
               FindFirstObjectByType<PlayerMovement>().TakeDamage(DMG);
                timer = 0f;
            }
            return;
        }
    }
    void RangedBehave()
    {
        if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            move = false;

            if (timer > attackCoolDown)
            {
                Instantiate(projectile, transform.position, transform.rotation).transform.GetComponent<Bullet>().dmg = DMG;
                timer = 0f;
            }
            return;
        }
        move = true;
    }
    void MoveTowardsPlayer()
    {
        if(move)transform.position = Vector2.MoveTowards(transform.position, player.position, 2f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") && kind == KindOfEnemy.Melee && timer >= attackCoolDown)
        {
            collision.transform.GetComponent<PlayerMovement>().TakeDamage(DMG);
            timer = 0f;
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if(HP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Instantiate(berryPrefab, transform.position, Quaternion.identity);
        AudioManager.instance.PlayEffect(deathSound);
        Destroy(gameObject);
    }
}
