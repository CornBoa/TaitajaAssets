using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    Transform playerTarget;
    public float attackSpeed = 5f,attackRange = 1f;
    float timer;
    public AudioClip chomp;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;      
    }
    void Update()
    {
        timer += Time.deltaTime;
        agent.SetDestination(playerTarget.position);
        if (timer >= attackSpeed && Vector3.Distance(transform.position,playerTarget.position) < attackRange)
        {
            timer = 0;
            Attack();
        }
    }
    void Attack()
    {
        FindFirstObjectByType<PlayerDasher>().TakeDMG();
        AudioManager.instance.PlayEffect(chomp);
    }
    private void OnEnable()
    {
        RandomizeChildScale();
    }
    void RandomizeChildScale()
    {
        foreach(Transform child in transform)
        {
            float randomScale = Random.Range(0.13f, 0.25f);
            child.localScale = new Vector3(randomScale, randomScale, randomScale);
        }
    }
}

