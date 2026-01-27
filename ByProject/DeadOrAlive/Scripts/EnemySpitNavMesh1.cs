using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpitNavMesh1 : MonoBehaviour , IDamagable
{
    NavMeshAgent agent;
    Movement player;
    [SerializeField] float HP, atkDist,DMG;
    [SerializeField] bool canAtk;
    [SerializeField] GameObject spitBallPrefab;
    [SerializeField] AudioClip death,hawkTuah;
    public void Died()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySound(death);
    }

    public void TakeDMG(float dmg)
    {
        HP -= dmg;
        if (HP <= 0)
        {
            Died();
        }
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.enabled = false;
        player = FindFirstObjectByType<Movement>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) < 15)
        {
            agent.enabled = true;
        }

        if (agent.enabled) agent.SetDestination(player.transform.position);
        else if(agent.enabled)agent.SetDestination(transform.position);

        Vector3 diff = player.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        if (Vector2.Distance(transform.position, player.transform.position) < atkDist && canAtk)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        AudioManager.instance.PlaySound(hawkTuah);
        canAtk = false;
        Instantiate(spitBallPrefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        canAtk = true;
    }
}