using System.Collections;
using UnityEngine;

public class EnemyNoNavMesh : MonoBehaviour
{
    BoxCollider BoxCollider;
    [SerializeField] Transform randPoint;
    CharacterController characterController;
    Transform player;
    bool canAttack = true;
    void Start()
    {
        BoxCollider = GetComponentInParent<BoxCollider>();
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomPos();
        StartCoroutine(WaitSec());
    }
    void Update()
    {
        Vector3 diff = randPoint.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        characterController.Move(transform.up * 2 * Time.deltaTime);               
        if (Vector3.Distance(transform.position, player.position) < 10) randPoint = player;
        else if (Vector3.Distance(transform.position, randPoint.position) < 1f) SetRandomPos();

    }
    public void SetRandomPos()
    {
        randPoint.position = new Vector3(
            Random.Range(BoxCollider.bounds.min.x, BoxCollider.bounds.max.x),
            Random.Range(BoxCollider.bounds.min.y, BoxCollider.bounds.max.y), 0
        );
    }
    void Attack()
    {
        player.GetComponent<Movement>().TakeDmg(25);
    }
    IEnumerator WaitSec()
    {        
       while (true)
        {
            yield return new WaitForSeconds(2);
            if (Vector3.Distance(transform.position, player.position) < 3 && canAttack) Attack();
        }

    }

}
