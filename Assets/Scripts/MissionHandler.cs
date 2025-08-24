using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _title;

    [SerializeField]
    private TMP_Text _waypointDescription;

    private int activeMission = 0;
    private int activeWaypoint = 0;
    private GameObject waypointCollider;
    private GameObject missionList;
    private GameObject missionObj;
    private GameObject waypointObj;
    
    // Start is called before the first frame update
    void Start()
    {
        missionList = GameObject.Find("Missions");
        waypointCollider = GameObject.Find("MissionWaypointCollider");
        activateMissionWaypoint(0, 0);
    }

    public void onPlayerReachedWaypoint()
    {
        Debug.Log("waypoint reached");
        //If the player has completed the final waypoint in the mission, move to next mission.
        if(activeWaypoint == missionObj.transform.childCount - 1)
        {
            //If the player has also completed the last mission, reset the mission counter.
            if(activeMission == missionList.transform.childCount - 1)
            {
                activateMissionWaypoint(0, 0);
            } 
            else
            {
                activateMissionWaypoint(activeMission + 1, 0);
            }
        } 
        else 
        {   
            activateMissionWaypoint(activeMission, activeWaypoint + 1);
        }
    }

    //Changes the active waypoint and mission to next in the list and moves the collider.
    void activateMissionWaypoint(int missionID, int waypointID)
    {
        if (missionList != null && missionList.transform.childCount > 0) {
            activeMission = missionID;
            activeWaypoint = waypointID;
            missionObj = missionList.transform.GetChild(missionID).gameObject;

            Mission mission = missionObj.GetComponent<Mission>();

            waypointObj = missionObj.transform.GetChild(waypointID).gameObject;
            Waypoint waypoint = waypointObj.GetComponent<Waypoint>();

            //Fires the dialogue for the current waypoint
            Debug.Log(waypoint.dialogue.name + waypoint.dialogue.sentences);
            FindObjectOfType<DialogueManager>().StartDialogue(waypoint.dialogue);

            _title.text = mission.missionName;
            _waypointDescription.text = waypoint.waypointDescription;

            waypointCollider.transform.position = waypointObj.transform.position;
        }
    }
}