using UnityEngine;

public class PedestrianWalker : MonoBehaviour
{
    public enum MoveDirection { Horizontal, Vertical }
    public MoveDirection direction = MoveDirection.Horizontal;

    public float moveDistance = 3f;   // How far to move from the starting point
    public float moveSpeed = 1.5f;    // Movement speed

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingForward = true;

    void Start()
    {
        startPos = transform.position;

        if (direction == MoveDirection.Horizontal)
            targetPos = startPos + Vector3.right * moveDistance;
        else
            targetPos = startPos + Vector3.up * moveDistance;
    }

    void Update()
    {
        Vector3 moveDir = (targetPos - transform.position).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Check if we've reached the target
        if (Vector3.Distance(transform.position, targetPos) < 0.05f)
        {
            // Flip direction
            movingForward = !movingForward;

            if (direction == MoveDirection.Horizontal)
                targetPos = startPos + (movingForward ? Vector3.right : Vector3.left) * moveDistance;
            else
                targetPos = startPos + (movingForward ? Vector3.up : Vector3.down) * moveDistance;
        }
    }
}
