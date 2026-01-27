using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour , IDamagable
{
    NavMeshAgent agent;
    Movement player;
    [SerializeField] float HP, atkDist,DMG;
    [SerializeField] bool canAtk;
    [SerializeField] AudioClip death;
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
        if(agent.enabled)agent.SetDestination(player.transform.position);

        Vector3 diff = agent.destination - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        if(agent.enabled)transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        if (Vector2.Distance(transform.position, player.transform.position) < atkDist && canAtk)
        {
            StartCoroutine(Attack());
        }
    }
    IEnumerator Attack()
    {
        canAtk = false;
        player.TakeDmg(DMG);
        yield return new WaitForSeconds(2);
        canAtk = true;
    }
}
public interface IDamagable
{
    public abstract void TakeDMG(float dmg);
    public abstract void Died();

}