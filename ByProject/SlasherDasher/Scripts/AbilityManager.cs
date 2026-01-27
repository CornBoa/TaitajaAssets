using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    
    public enum AbilityType
    {
        Teleport,
        Shield,
        Nuke,Medkit
    }   
    public bool isAbilityActive = false;
    public AbilityType currentAbility;
    public GameObject abilityIcon;
    public Image abilityUI;
    public Sprite nuke, teleport, shield;
    public GameObject deathEffect;
    public LayerMask wall;
    private void Start()
    {
        abilityIcon.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(1) && isAbilityActive)
        {
            ActivateAbility();
        }
    }
    public void SetAbility(AbilityType ability)
    {
        currentAbility = ability;
        isAbilityActive = true;
        abilityIcon.SetActive(true);
        switch(ability)
        {
            case AbilityType.Teleport:
                abilityUI.sprite = teleport;
                break;
            case AbilityType.Shield:
                abilityUI.sprite = shield;
                break;
            case AbilityType.Nuke:
                abilityUI.sprite = nuke;
                break;
        }
    }
    void ActivateAbility()
    {
        switch(currentAbility)
        {
            case AbilityType.Teleport:
                Vector3 pos;
                if (Physics.Linecast(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), out RaycastHit hit, wall))
                {
                    pos = hit.point;
                    break;
                }
                else pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = transform.position.z;
                transform.position = pos;
                break;
            case AbilityType.Shield:
                PlayerDasher player = GetComponent<PlayerDasher>();
                player.Invincibility(10f);
                break;
            case AbilityType.Nuke:
                foreach(GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    if(Camera.main.WorldToScreenPoint(enemy.transform.position).y > 0 &&
                       Camera.main.WorldToScreenPoint(enemy.transform.position).y < Screen.height &&
                       Camera.main.WorldToScreenPoint(enemy.transform.position).x > 0 &&
                       Camera.main.WorldToScreenPoint(enemy.transform.position).x < Screen.width)
                    {
                        enemy.SetActive(false);
                        Instantiate(deathEffect, enemy.transform.position, Quaternion.identity);
                    }                  
                }
                FindAnyObjectByType<Camera2D>().TriggerShake(0.3f, 0.6f);
                break;
            case AbilityType.Medkit:
                GetComponent<PlayerDasher>().Heal();
                break;
        }
        abilityIcon.SetActive(false);
        isAbilityActive = false;
    }
}
