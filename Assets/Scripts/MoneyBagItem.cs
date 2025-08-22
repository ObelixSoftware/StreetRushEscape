using UnityEngine;

public class MoneyBagItem : MonoBehaviour
{
    private void Start()
    {
        Scoreboard.Instance?.RegisterBag(); // count total bags on map
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Car")) // changed from "Player" to "Car"
        {
            Scoreboard.Instance?.CollectBag(); // increment collected count
            Destroy(gameObject); // remove bag from scene
        }
    }
}
