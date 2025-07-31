using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCarCollisionSensor : MonoBehaviour
{
    [Header("Sensor Settings")]
    public bool isLeftSensor = true;

    private CopDriveHandler copDriveHandler;
    private int collisionActive = 0;

    // Start is called before the first frame update
    void Start()
    {
        copDriveHandler = GetComponentInParent<CopDriveHandler>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        collisionActive++;
        if (collisionActive == 1) {
            copDriveHandler.OnSensorTriggered(isLeftSensor, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        collisionActive--;
        if (collisionActive == 0) {
            copDriveHandler.OnSensorTriggered(isLeftSensor, false);
        }
    }
}
