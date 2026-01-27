using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public enum KindOfEnemy
    {
        Melee,
        Ranged
    }
    public KindOfEnemy kind;
    public int DMG, HP;
    public float attackCoolDown, attackRange = 5f;
    float timer;
    Transform player;
    public GameObject projectile;
    public bool move = true;
    public AudioClip deathSound;
    public bool followSomewhere = false;
    public Transform destination;
    NavMeshAgent agent;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
    void Update()
    {
        if (timer < attackCoolDown)
        {
            timer += Time.deltaTime;
        }
        Move();
        if (KindOfEnemy.Ranged == kind)
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
            }
            return;
        }
        move = true;
    }
    void Move()
    {
        if (followSomewhere)
        {

            agent.SetDestination(destination.position);
            if (Vector3.Distance(transform.position, destination.position) < 1f)
            {
                gameObject.SetActive(false);
            }
        }
        else
            agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && kind == KindOfEnemy.Melee && timer >= attackCoolDown)
        {
            ResourcesGa.instance.tiredness -= DMG;
        }
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        AudioManager.instance.PlayEffect(deathSound);
        Destroy(gameObject);
    }
}
