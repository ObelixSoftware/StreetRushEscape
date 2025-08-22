using UnityEngine;

public class Mission2Trigger : MonoBehaviour
{
    public MissionCutsceneController cutsceneController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            Debug.Log("Car entered trigger!");
            cutsceneController.StartCutscene();
            gameObject.SetActive(false); // Disable trigger after use
        }
    }
}
