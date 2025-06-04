using UnityEngine;

public class TrafficLightZone : MonoBehaviour
{
    public TrafficLightController trafficLight;

    public int GetLightState()
    {
        return trafficLight != null ? trafficLight.GetLightState() : 1; // default to green
    }
}
