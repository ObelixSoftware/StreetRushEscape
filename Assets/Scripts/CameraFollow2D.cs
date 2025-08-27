using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // <--- THIS is the field you should see
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Zoom Settings")]
    public float minZoom = 5f;     // zoomed in at low speed
    public float maxZoom = 10f;    // zoomed out at high speed
    public float zoomLerpSpeed = 5f;
    public float speedForMaxZoom = 20f; // what speed counts as "full zoom out"

    private Camera camera;
    private Rigidbody2D targetRb;

    void Awake()
    {
        camera = GetComponent<Camera>();
        if (target != null)
            targetRb = target.GetComponent<Rigidbody2D>();
    }
    void LateUpdate()
    {
        if (target == null) return;

        //Follow logic
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        //Zoom logic
        if (targetRb != null)
        {
            float speed = targetRb.velocity.magnitude;
            float t = Mathf.Clamp01(speed / speedForMaxZoom);
            float targetZoom = Mathf.Lerp(minZoom, maxZoom, t);

            camera.orthographicSize = Mathf.Lerp(camera.orthographicSize, targetZoom, zoomLerpSpeed * Time.deltaTime);
        }
    }
}
