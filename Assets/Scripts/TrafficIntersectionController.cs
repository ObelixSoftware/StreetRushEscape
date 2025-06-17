using UnityEngine;

public class TrafficIntersectionController : MonoBehaviour
{
    public TrafficLightController northLight;
    public TrafficLightController southLight;
    public TrafficLightController eastLight;
    public TrafficLightController westLight;

    public float greenDuration = 5f;
    public float yellowDuration = 2f;

    private enum Phase { NorthSouthGreen, EastWestGreen, Yellow }
    private Phase currentPhase = Phase.NorthSouthGreen;

    private float timer = 0f;

    void Start()
    {
        SetPhase(Phase.NorthSouthGreen);
    }

    void Update()
    {
        timer += Time.deltaTime;

        switch (currentPhase)
        {
            case Phase.NorthSouthGreen:
                if (timer >= greenDuration)
                    SetPhase(Phase.Yellow);
                break;

            case Phase.EastWestGreen:
                if (timer >= greenDuration)
                    SetPhase(Phase.Yellow);
                break;

            case Phase.Yellow:
                if (timer >= yellowDuration)
                {
                    if (northLight != null && northLight.GetLightState() == 1)
                        SetPhase(Phase.EastWestGreen);
                    else
                        SetPhase(Phase.NorthSouthGreen);
                }
                break;
        }
    }

    void SetPhase(Phase phase)
    {
        Debug.Log($"SetPhase called. North: {northLight}, South: {southLight}, East: {eastLight}, West: {westLight}");

        timer = 0f;
        currentPhase = phase;

        if (phase == Phase.NorthSouthGreen)
        {
            northLight?.SetLight(1); // green
            southLight?.SetLight(1);
            eastLight?.SetLight(0);  // red
            westLight?.SetLight(0);
        }
        else if (phase == Phase.EastWestGreen)
        {
            eastLight?.SetLight(1);
            westLight?.SetLight(1);
            northLight?.SetLight(0);
            southLight?.SetLight(0);
        }
        else if (phase == Phase.Yellow)
        {
            northLight?.SetLight(2);
            southLight?.SetLight(2);
            eastLight?.SetLight(2);
            westLight?.SetLight(2);
        }
    }
}
