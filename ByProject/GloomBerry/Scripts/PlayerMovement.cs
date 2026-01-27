using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float speed = 5f;
    public float currentHP, maxHP;
    public TextMeshProUGUI hpText;
    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        hpText.text = currentHP.ToString() + " / " + maxHP.ToString();
    }  
    void Update()
    {
        hpText.text = currentHP.ToString("0.0") + " / " + maxHP.ToString("0.0");
        Move();
    }
    void Move()
    {
        Vector3 movement = Vector3.zero;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        if (x != 0 || y != 0)
        {
            animator.SetBool("Walk", true);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
        movement.x = x;
        movement.y = y;
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(3,3,3);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-3,3,3);
        }
        controller.Move(movement * Time.deltaTime * speed);
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }   
    void Die()
    {
        FindFirstObjectByType<FadeInOut>().FadeOut();
    }
}
