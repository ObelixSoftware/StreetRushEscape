using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitArrowController : MonoBehaviour
{
    [Header("Arrow Settings")]
    public RectTransform arrowRect;       // Drag your arrow UI image here
    public Transform playerCar;           // The car transform
    public Transform exitPoint;           // The exit point in the world
    public float delay = 40f;             // Delay before showing arrow

    private CanvasGroup arrowCanvasGroup;

    private bool missionActive = false;

    private void Awake()
    {
        if (arrowRect == null)
        {
            Debug.LogError("Arrow RectTransform is not assigned!");
            return;
        }

        // Use CanvasGroup to show/hide arrow smoothly
        arrowCanvasGroup = arrowRect.GetComponent<CanvasGroup>();
        if (arrowCanvasGroup == null)
        {
            arrowCanvasGroup = arrowRect.gameObject.AddComponent<CanvasGroup>();
        }

        // Start hidden
        arrowCanvasGroup.alpha = 0f;
        arrowRect.gameObject.SetActive(true); // keep active for rotation
    }

    private void Update()
    {
        if (!missionActive) return;

        if (playerCar == null || exitPoint == null) return;

        // Direction from car to exit
        Vector3 direction = exitPoint.position - playerCar.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate arrow
        arrowRect.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    public void StartMissionArrow()
    {
        if (missionActive) return;
        missionActive = true;
        StartCoroutine(ShowArrowAfterDelay());
    }

    private IEnumerator ShowArrowAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        arrowCanvasGroup.alpha = 1f; // show arrow
    }
}
