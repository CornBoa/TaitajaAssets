using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    public float movementSpeed, jump, gravity, groundCheck,rotSpeed,dashSpeed,dashCooldown,dashTime,wallCheck;
    bool grounded,dashing,doubleJumped,walled,canDash = true;
    public bool ouchie;
    float moveDir;
    public LayerMask ground, wall;
    public GameObject gCheck,playerVisuals,trailPrefab;
    Quaternion savedRotation;  
    InputSystem_Actions inputActions;
    public AnimationCurve dashCurve;
    TrailRenderer tr;
    Animator animator;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        savedRotation = playerVisuals.transform.rotation;
        inputActions = new InputSystem_Actions();
        tr = GetComponent<TrailRenderer>();
        animator = GetComponentInChildren<Animator>();
        inputActions.Player.Enable();
        inputActions.Player.Jump.performed += Jump;
        inputActions.Player.Sprint.performed += Dash;
    }
    void Update()
    {
        CheckIfGrounded();
        if (rigidBody2D.linearVelocity.y < 0f)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Baller", false);
            animator.SetBool("Landing", true);          
        }
        else if (grounded)
        {
            animator.SetBool("Jumping", false);
            animator.SetBool("Baller", false);
            animator.SetBool("Landing", false);
        }
        if (walled)
        {
            if (Physics2D.Raycast(transform.position, transform.right, 1, wall))
            {
                playerVisuals.transform.localScale = new Vector2(-2, 2);
            }
            else
            {
                playerVisuals.transform.localScale = new Vector2(2, 2);
            }
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Dash(InputAction.CallbackContext ctx)
    {
        StartCoroutine(Dashing());
    }
    IEnumerator Dashing()
    {
        if(!canDash)
        {
            yield break;
        }
        canDash = false;
        Transform trail = Instantiate(trailPrefab, transform.position, Quaternion.identity,transform).transform;
        float timer = 0f;   
        dashing = true; 
        animator.SetBool("Dashing", true);
        while (timer < 1)
        {
            rigidBody2D.linearVelocity = new Vector2(dashCurve.Evaluate(timer) * moveDir * dashSpeed, 0);
            yield return null;
            timer += Time.deltaTime * dashTime;
        }
        dashing = false;
        trail.SetParent(null);
        new WaitForSeconds(dashCooldown);
        canDash = true;
        animator.SetBool("Dashing", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnWall();
        if (dashing && collision.gameObject.layer == 7)
        {
            ouchie = false;
            StopCoroutine(Dashing());
        }
        else if (dashing && collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<BreakableWall>().BreakIt();
        }
    }

    void Move()
    {
        Vector2 movement = new Vector3(inputActions.Player.Move.ReadValue<Vector2>().x, 0);
        if (movement.magnitude <= 0)
        {
            animator.SetBool("Running", false);
        }
        if (dashing) return;
      
        if (movement.magnitude > 0)
        {
            animator.SetBool("Running", true);
            if (movement.x > 0)
            {
                moveDir = 1;    
                playerVisuals.transform.localScale = new Vector2(2, 2);
            }
            else
            {
                moveDir = -1;
                playerVisuals.transform.localScale = new Vector2(-2, 2);
            }
            movement.y = rigidBody2D.linearVelocity.y;
            rigidBody2D.linearVelocityX = movement.x * movementSpeed * Time.deltaTime;
        }
        else if (!walled && !ouchie)
        {
            rigidBody2D.linearVelocityX = 0;
        }
    }
    public void KnockBack(Vector2 direction, float stgt)
    {
        rigidBody2D.AddForce(direction * stgt, ForceMode2D.Impulse);
    }
    void CheckIfGrounded()
    {
        if (Physics2D.OverlapCircle(gCheck.transform.position, groundCheck, ground))
        {
            grounded = true;
            walled = false;
            doubleJumped = false;
            ouchie = false;
            return;
        }
        grounded = false;
    }
    void CheckIfOnWall()
    {
        if(Physics2D.OverlapCircle(transform.position, wallCheck, wall) && !grounded)
        {
            if(!animator.GetBool("Walled"))animator.SetBool("Walled", true);
            rigidBody2D.linearDamping = 10;
            walled = true;
            doubleJumped = false;
            return;
        }
        else if(grounded)
        {
            animator.SetBool("Walled", false);
            walled = false;                 
        }
        else
        {
            animator.SetBool("Walled", false);
            rigidBody2D.linearDamping = 0;
        }
      
    }
    void Jump(InputAction.CallbackContext ctx)
    {
        if (grounded && !walled)
        {
            rigidBody2D.AddForce(Vector2.up * jump, ForceMode2D.Impulse);          
            animator.SetBool("Jumping", true);
            animator.Play("Jump");
            return;
        }
        else if (!doubleJumped && !walled)
        {
            doubleJumped = true;
            rigidBody2D.AddForce(Vector2.up * jump * 1.2f, ForceMode2D.Impulse);
            StartCoroutine(Spin());
        }      
        else if (walled)
        {
            if (Physics2D.Raycast(transform.position, transform.right, 1, wall))
            {
                rigidBody2D.linearDamping = 0;
                animator.SetBool("Walled", false);
                animator.Play("Jump");
                rigidBody2D.AddForce(new Vector2(-1 * jump, jump * 1.5f), ForceMode2D.Impulse);
                playerVisuals.transform.localScale = new Vector2(-2, 2);
            }
            else
            {
                rigidBody2D.linearDamping = 0;
                animator.SetBool("Walled", false);
                animator.Play("Jump");
                rigidBody2D.AddForce(new Vector2(jump, jump * 1.5f), ForceMode2D.Impulse);
                playerVisuals.transform.localScale = new Vector2(2, 2);
            }
        }
    }
    void OnDestroy()
    {
        inputActions.Player.Jump.performed -= Jump;
        inputActions.Player.Sprint.performed -= Dash;
        inputActions.Player.Disable();
    }
    IEnumerator Spin()
    {
        animator.SetBool("Baller", true);
        while (!walled && !grounded)
        {
            playerVisuals.transform.Rotate(0, 0, rotSpeed * Time.deltaTime * moveDir);
            yield return null;
            if (grounded ||rigidBody2D.linearVelocity.y < 0f || walled)
            {
                break;
            }
        }
        float timer = 0f;
        while (timer < 1f)
        {           
            playerVisuals.transform.rotation = Quaternion.Slerp(playerVisuals.transform.rotation, savedRotation, timer);
            timer += Time.deltaTime * 3f;
            yield return null;
        }
        yield return new WaitUntil(() => grounded || walled);
        animator.SetBool("Landing", false);
    }
}
