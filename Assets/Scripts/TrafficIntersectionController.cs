using UnityEngine;

public class TrafficIntersectionController : MonoBehaviour
{
    public TrafficLightController northLight;
    public TrafficLightController southLight;
    public TrafficLightController eastLight;
    public TrafficLightController westLight;

    public PedestrianLightController pedestrianNorth;
    public PedestrianLightController pedestrianSouth;
    public PedestrianLightController pedestrianEast;
    public PedestrianLightController pedestrianWest;

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
        timer = 0f;
        currentPhase = phase;

        switch (phase)
        {
            case Phase.NorthSouthGreen:
                // Traffic
                northLight?.SetLight(1); // Green
                southLight?.SetLight(1);
                eastLight?.SetLight(0);  // Red
                westLight?.SetLight(0);

                // Pedestrians (cross East-West)
                pedestrianNorth?.SetLight(false); // Don't cross parallel
                pedestrianSouth?.SetLight(false);
                pedestrianEast?.SetLight(true);   // Safe to cross
                pedestrianWest?.SetLight(true);
                break;

            case Phase.EastWestGreen:
                // Traffic
                eastLight?.SetLight(1);
                westLight?.SetLight(1);
                northLight?.SetLight(0);
                southLight?.SetLight(0);

                // Pedestrians (cross North-South)
                pedestrianNorth?.SetLight(true);
                pedestrianSouth?.SetLight(true);
                pedestrianEast?.SetLight(false);
                pedestrianWest?.SetLight(false);
                break;

            case Phase.Yellow:
                // All lights yellow
                northLight?.SetLight(2);
                southLight?.SetLight(2);
                eastLight?.SetLight(2);
                westLight?.SetLight(2);

                // All pedestrians red
                pedestrianNorth?.SetLight(false);
                pedestrianSouth?.SetLight(false);
                pedestrianEast?.SetLight(false);
                pedestrianWest?.SetLight(false);
                break;
        }
    }
}
