using UnityEngine;
using UnityEngine.UI;

public class ExitZone : MonoBehaviour
{
    [Header("Exit Settings")]
    public GameObject exitIndicator; // Drag a 3D/2D arrow, UI icon, or marker here
    public string playerTag = "Car"; // Tag of the player's car

    private bool playerInZone = false;

    private void Start()
    {
        if (exitIndicator != null)
            exitIndicator.SetActive(true); // Show the indicator at start
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInZone = true;
            OnPlayerExitReached();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            playerInZone = false;
        }
    }

    private void OnPlayerExitReached()
    {
        Debug.Log("Player reached the exit zone!");

        // Optional: hide the indicator when player reaches it
        if (exitIndicator != null)
            exitIndicator.SetActive(false);

        // Call the other guy's function to display the score
        // Example (replace with actual function):
        // ScoreDisplay.Instance.ShowScore();

        // Optional: stop car movement
        Rigidbody2D rb = GameObject.FindGameObjectWithTag(playerTag)?.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.velocity = Vector2.zero;

        // Optional: disable player controls here
        // PhysicsPlayerCarController carController = GameObject.FindGameObjectWithTag(playerTag)?.GetComponent<PhysicsPlayerCarController>();
        // if (carController != null)
        //     carController.enabled = false;
    }
}
