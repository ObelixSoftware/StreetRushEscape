using UnityEngine;

public class MissionAreaCheck : MonoBehaviour
{
    public Transform car;
    public float radius = 5f;
    public bool missionStarted = false;

    public MissionCutsceneController cutsceneController; // assign in inspector
    public ExitArrowController exitArrowController;      // assign in inspector

    void Update()
    {
        if (missionStarted) return;

        float distance = Vector3.Distance(transform.position, car.position);

        if (distance <= radius)
        {
            missionStarted = true;
            Debug.Log("Mission Started!");

            cutsceneController.StartCutscene();

            // Trigger exit arrow logic
            if (exitArrowController != null)
            {
                exitArrowController.StartMissionArrow();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
