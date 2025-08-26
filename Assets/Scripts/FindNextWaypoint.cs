using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNextWaypoint : MonoBehaviour
{
    private GameObject nextWaypoint;
    private GameObject playerCar;

    // Start is called before the first frame update
    void Start()
    {
        nextWaypoint = GameObject.Find("MissionWaypointCollider");
        playerCar = GameObject.Find("Car");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 carPosition = playerCar.transform.position;
        Vector2 waypointPosition = nextWaypoint.transform.position;
        Vector2 direction = waypointPosition - carPosition;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //The arrow doesn't point up by default.
        angle -= 90f;

        Vector3 targetEuler = new Vector3(0, 0, angle);
        transform.eulerAngles = targetEuler;
    }
}