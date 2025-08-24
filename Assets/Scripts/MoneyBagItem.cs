using UnityEngine;

public class MoneyBagItem : MonoBehaviour
{
    [Header("Gameplay")]
    public GameController gameController;   // Assign your GameController here
    public float pursuitIncrease = 5f;      // How much pursuit increases per bag

    private void Start()
    {
        Scoreboard.Instance?.RegisterBag(); // Count total bags on map
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car")) // Only trigger when car touches
        {
            // Increment collected bags
            Scoreboard.Instance?.CollectBag();

            // Increase pursuit
            if (gameController != null)
            {
                gameController.IncreasePursuit(pursuitIncrease);
            }

            // Destroy the bag
            Destroy(gameObject);
        }
    }
}
