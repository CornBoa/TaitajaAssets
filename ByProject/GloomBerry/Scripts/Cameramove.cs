using UnityEngine;

public class Cameramove : MonoBehaviour
{
    Transform player;
    [SerializeField] float smoothSpeed = 0.125f;    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        CameraLerpFollow();
    }
    void CameraLerpFollow()
    {
        float timer = 0f;   
        Vector3 desiredPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * timer);
        transform.position = smoothedPosition;
        timer += Time.deltaTime;
    }
}
