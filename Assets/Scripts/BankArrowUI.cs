using UnityEngine;
using UnityEngine.UI;

public class BankArrowUI : MonoBehaviour
{
    public Transform player;
    public Transform bank;    

    private RectTransform arrowRect;

    void Awake()
    {
        arrowRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (player == null || bank == null) return;

        // Get direction from player to bank
        Vector3 dir = bank.position - player.position;

        // Ignore vertical
        dir.y = 0f;

        // Calculate angle
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        // Rotate the UI arrow
        arrowRect.localRotation = Quaternion.Euler(0, 0, -angle);
    }
}
