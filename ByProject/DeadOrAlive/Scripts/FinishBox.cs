using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
