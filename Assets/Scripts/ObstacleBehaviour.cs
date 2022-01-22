using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene 

public class ObstacleBehaviour : MonoBehaviour
{

    [Tooltip("How long to wait before restarting the game")]
    public float waitTime = 2.0f;

    private void Start()
    {
        InvokeRepeating("CheckPlayer", 2, 2);
    }

    private void CheckPlayer()
    {
        if (PlayerMovement.Instance.transform.position.z - gameObject.transform.position.z > 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            collision.gameObject.GetComponent<PlayerMovement>().Kill();
        }
    }

    
}