using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    CharacterController characterController;
    Transform cameraTransform;
    [SerializeField] float speed;
    [SerializeField]Transform rot;
    [SerializeField] float HP,maxHp;
    [SerializeField] Slider slider;
    [SerializeField]List<AudioClip> hurt;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        InvokeRepeating("Heal",0,15);
        Cursor.lockState = CursorLockMode.Confined;
    }
    void Update()
    {
        Move();
        slider.value = HP;
    }
    public void TakeDmg(float takenDMG)
    {
        FindFirstObjectByType<ScreenShake>().StartShake(0.1f, 0.2f);
        AudioManager.instance.PlaySound(hurt[Random.Range(0,hurt.Count-1)]);
        HP -= takenDMG;
        if (HP <= 0) SceneManager.LoadScene(4);
    }
    public void Heal(float amount)
    {
        HP += amount;
        if(HP > maxHp)HP = maxHp;
    }
    void Move()
    {
        Vector3 movement = new Vector3();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        characterController.Move(movement * Time.deltaTime * speed);
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        rot.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
