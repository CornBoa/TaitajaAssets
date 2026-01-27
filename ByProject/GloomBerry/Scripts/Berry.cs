using System.Collections;
using UnityEngine;

public class Berry : MonoBehaviour
{
    
    public float givenXP = 10f;
    SpriteRenderer sprite;
    public Color small, normal, rare;
    public AudioClip nom;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (Random.Range(0, 100) > 90)
        {
            sprite.color = rare;
            givenXP = 20f;
        }
        else if (Random.Range(0, 100) > 70)
        {
            sprite.color = normal;
            givenXP = 10f;
        }
        else
        {
            givenXP = 5f;
            sprite.color = small;
        }
    }

    public void onPickup()
    {       
        StartCoroutine(moveToPlayer());
    }
    IEnumerator moveToPlayer()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, FindFirstObjectByType<PlayerMovement>().transform.position) < 2f)
            {
                transform.position = Vector3.MoveTowards(transform.position, FindFirstObjectByType<PlayerMovement>().transform.position, 10f * Time.deltaTime);
            }
            if(Vector3.Distance(transform.position, FindFirstObjectByType<PlayerMovement>().transform.position) < 0.1f)
            {
                break;
            }
            yield return null;
        }
        AudioManager.instance.PlayEffect(nom);
        StatsManager.instance.AddXP(givenXP);
        Destroy(gameObject);
    }
}
