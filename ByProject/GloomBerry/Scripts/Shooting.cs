using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootCooldown = 0.5f, distance, maxEnemyDist;
    public int DMG, bulletAmount = 3;
    public Vector2 spread = new Vector2(0.1f, 0.1f);
    public Transform target, shotgun;
    public GameObject trail;
    public LayerMask enemy;
    public AudioClip shootSound;
    void Start()
    {
        StartCoroutine(Shooter());
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            ShotgunShooting();
        }
        if(target != null)shotgun.rotation = Quaternion.LookRotation(Vector3.forward, target.position - shotgun.position);
    }
    void ShotgunShooting()
    {
        if(Physics2D.OverlapCircle(transform.position, maxEnemyDist, enemy) == null)
        {
            return;
        }
        else if (Physics2D.OverlapCircle(transform.position, maxEnemyDist, enemy).transform != null)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, maxEnemyDist, enemy);
            if (target != null) foreach (Collider2D en in enemies)
                { if (Vector3.Distance(en.transform.position, transform.position) < Vector3.Distance(target.position, transform.position)) target = Physics2D.OverlapCircle(transform.position, maxEnemyDist, enemy).transform; }
            else target = Physics2D.OverlapCircle(transform.position, maxEnemyDist, enemy).transform;

        }
        RaycastHit2D hit;
        AudioManager.instance.PlayEffect(shootSound);
        for (int i = 0; i < bulletAmount; i++)
        {
            
            Vector2 direction = (target.position - transform.position);
            direction.x += Random.Range(-spread.x, spread.x);
            direction.y += Random.Range(-spread.y, spread.y);
            hit = Physics2D.Raycast(transform.position, direction.normalized,distance,enemy);
            BulletTrail boolet = Instantiate(trail, transform.position, transform.rotation).GetComponent<BulletTrail>();
            if (hit.collider != null)
            {
                boolet.Setup(hit.transform);
                hit.transform.GetComponent<Enemy>().TakeDamage(DMG);
            }
            else
            {
                Vector3 missTarget = (Vector3)direction.normalized * distance + transform.position;
                GameObject tempTarget = new GameObject();
                tempTarget.transform.position = missTarget;
                boolet.Setup(tempTarget.transform);
                Destroy(tempTarget, 2f);
            }
        }
    }
    IEnumerator Shooter()
    {
        while (true)
        {
            ShotgunShooting();
            yield return new WaitForSeconds(shootCooldown);
        }
    }
}
