using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetOnCollision : MonoBehaviour
{
    // Reference to the floor GameObject
    public GameObject floor;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the 'cube' tag and it's colliding with the floor
        if (collision.gameObject.CompareTag("cube") && collision.gameObject == floor)
        {
            // Reset the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}