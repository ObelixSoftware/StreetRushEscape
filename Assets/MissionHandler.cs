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
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject missionList = GameObject.Find("Missions");
        GameObject missionObj = missionList.transform.GetChild(activeMission).gameObject;

        Mission mission = missionObj.GetComponent<Mission>();

        GameObject waypointObj = missionObj.transform.GetChild(activeWaypoint).gameObject;
        Waypoint waypoint = waypointObj.GetComponent<Waypoint>();

        _title.text = mission.missionName;
        _waypointDescription.text = waypoint.waypointDescription;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
