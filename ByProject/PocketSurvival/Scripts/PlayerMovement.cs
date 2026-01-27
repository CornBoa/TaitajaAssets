using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    public float speed = 5f;
    public float currentHP, maxHP;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHP = maxHP;
    }  
    void Update()
    {
        Move();
    }
    void Move()
    {
        Vector3 movement = Vector3.zero;
        float y = Input.GetAxisRaw("Vertical");
        float x = Input.GetAxisRaw("Horizontal");
        movement.x = x;
        movement.y = y;
        if (movement.x > 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
        else if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        controller.Move(movement * Time.deltaTime * speed);
    } 
    public void TakeDamage(float damage)
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
