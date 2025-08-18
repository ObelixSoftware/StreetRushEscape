using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionWaypointColliderHandler : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Car"))
        {
            GameObject missionWindow = GameObject.Find("Mission Window");
            MissionHandler missionHandler = missionWindow.GetComponent<MissionHandler>();
            missionHandler.onPlayerReachedWaypoint();
        }
    }
}