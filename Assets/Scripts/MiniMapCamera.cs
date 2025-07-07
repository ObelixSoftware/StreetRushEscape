using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    private Transform car;

    void Start()
    {
        GameObject carObject = GameObject.FindGameObjectWithTag("Car");
        if (carObject != null)
        {
            car = carObject.transform;
        }
        else
        {
            Debug.LogWarning("No GameObject with tag 'Car' found.");
        }
    }

    void LateUpdate()
    {
        if (car != null)
        {
            Vector3 newPosition = car.position;
            newPosition.z = -10f; // Keep camera distance for 2D
            transform.position = newPosition;
        }
    }
}
