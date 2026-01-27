using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDasher : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    public  float dashCooldown = 1f;
    public Vector3 target;
    public Image targetIndicator;
    bool canDash = true, invincible = false;
    int maxHP = 3, currentHp;
    public GameObject deathEffect,floaty;
    SpriteRenderer character;
    public List<GameObject> abilities,hpLogs;
    bool tutor = true;
    public AudioClip dashSound,slashSound,dmgTakenSound,deathSound;
    public Image bloodVignette;
    void Start()
    {
        currentHp = maxHP;
        character = GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canDash)
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = transform.position.z;
            StartCoroutine(Dash());
        }
    }
    IEnumerator Dash()
    {      
        Vector3 savedPos = transform.position;
        canDash = false;
        targetIndicator.fillAmount = 0f;
        float timer = 0f;       
        AudioManager.instance.PlayEffect(dashSound);
        while (timer < dashDuration)
        {
            Vector3 direction = (target - transform.position).normalized;
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, 1f))
            {
                if(hit.collider.CompareTag("Wall")) break;

            }      
            transform.position += direction * dashSpeed * Time.deltaTime;
            timer += Time.deltaTime;          
            yield return null;
        }
        if (Physics.Linecast(transform.position,savedPos))
        {          
            int i = 0;
            foreach (RaycastHit col in Physics.RaycastAll(transform.position, savedPos - transform.position))
            {                
                if (col.collider.tag == "Enemy")
                {
                    col.collider.gameObject.SetActive(false);
                    i++;
                    int score = 10 * i;
                    ScoreSaver.instance.AddScore(score);
                    FindFirstObjectByType<Camera2D>().TriggerShake(0.2f, 0.25f);
                    Instantiate(deathEffect, col.collider.transform.position, Quaternion.identity);
                    AudioManager.instance.PlayEffect(slashSound);
                    Instantiate(floaty, Camera.main.WorldToScreenPoint(col.collider.transform.position), Quaternion.identity, FindAnyObjectByType<Canvas>().transform).GetComponent<FloatyText>().SetScore(score);
                    if (tutor ||Random.value < 0.01f)
                    {
                        GameObject ability = abilities[Random.Range(0, abilities.Count)];
                        Instantiate(ability, col.collider.transform.position, Quaternion.identity);                      
                        tutor = false;
                    }
                }
                else if(col.collider.tag == "Ability")
                {
                    Destroy(col.collider.gameObject);
                }
            }
        }
        timer = 0f;
        while (timer < dashCooldown)
        {
            timer += Time.deltaTime;
            targetIndicator.fillAmount = timer / dashCooldown;
            yield return null;
        }
        canDash = true;
    }
    public bool Invincibility(float time)
    {
        if (invincible) return false;
        else
        {
            invincible = true;
            StartCoroutine(InvincibilityTimer(time));
            return true;
        }
    }
    IEnumerator InvincibilityTimer(float duration)
    {
        float i = duration;
        character.color = Color.white;
        while (duration > 0f)
        {
            character.color = Color.clear;
            yield return new WaitForSeconds(0.1f);
            character.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            i -= 0.2f;
        }
        invincible = false;
    }
    [ContextMenu ("Take Damage")]
    public void TakeDMG()
    {
        if(invincible || !canDash) return;
        currentHp--;
        FindAnyObjectByType<Camera2D>().TriggerShake(0.3f, 0.4f);
        AudioManager.instance.PlayEffect(dmgTakenSound);
        for (int i = maxHP - 1; i >= 0; i--)
        {
            if(i >= currentHp)
            {
                hpLogs[i].SetActive(false);
            }
        }
        if (currentHp <= 0)
        {
            FindFirstObjectByType<FadeInOut>().onFadeOut.AddListener(delegate { SceneManager.LoadScene(2); });
            FindFirstObjectByType<FadeInOut>().FadeOut();
            AudioManager.instance.PlayEffect(deathSound);
        }
    }
    public void Heal()
    {
        currentHp = maxHP;
        for (int i = maxHP - 1; i >= 0; i--)
        {
            if (i >= currentHp)
            {
                hpLogs[i].SetActive(false);
            }
        }
    }
}
