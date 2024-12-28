using UnityEngine;

public class Wander2 : MonoBehaviour
{
    public float speed = 50f; // Movement speed
    public float roamingTime = 3f; // Time before changing direction
    public float detectionRadius = 2f; // How far to check for obstacles (e.g., table)
    public LayerMask obstacleLayer; // Define obstacles (the table in this case)
    public Rect boundary; // Define the allowed movement area (x, y, width, height)

    private Vector2 movementDirection;
    private float timer;
    private Draggable draggableScript;

    private void Awake()
    {
        draggableScript = GetComponent<Draggable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!draggableScript.isDragged && !draggableScript.IsAtTable())
        {
            // Timer for roaming
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                SetRandomDirection();
                timer = roamingTime;
            }

            // Move the character while avoiding obstacles
            MovePerson();
        }
    }

    private void SetRandomDirection()
    {
        // Generate a random direction (normalized to avoid very fast diagonal movement)
        movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    private void MovePerson()
    {
        // Vector3 newPosition = transform.position + (Vector3)movementDirection * speed * Time.deltaTime;

        // // Check for boundary restrictions
        // newPosition.x = Mathf.Clamp(newPosition.x, boundary.xMin, boundary.xMax);
        // newPosition.y = Mathf.Clamp(newPosition.y, boundary.yMin, boundary.yMax);

        Vector3 newPosition = transform.position + (Vector3)movementDirection * speed * Time.deltaTime;

        // Check for boundary restrictions
        if (newPosition.x < boundary.xMin || newPosition.x > boundary.xMax || 
            newPosition.y < boundary.yMin || newPosition.y > boundary.yMax)
        {
            // Adjust direction to move towards the center if near boundary
            Debug.Log("Hit a wall, changing direction");
            movementDirection = GetDirectionTowardCenter(newPosition);
            return; // Skip movement this frame to prevent overshooting
        }
        // Check for obstacles in the movement direction
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRadius, movementDirection, detectionRadius, obstacleLayer);
        if (hit.collider != null)
        {
            Debug.Log("Collision detected");
             // Adjust direction to move away from the obstacle
            movementDirection = GetDirectionAwayFromObstacle(hit.normal);
            return; // Skip movement this frame to prevent collision
        }

        // Apply the movement
        transform.position = newPosition;
    }

      private Vector2 GetDirectionTowardCenter(Vector3 position)
    {
        // Calculate a vector pointing from the current position to the center of the boundary
        Vector2 boundaryCenter = (boundary.min + boundary.max) / 2;
        return (boundaryCenter - (Vector2)position).normalized;
    }

    private Vector2 GetDirectionAwayFromObstacle(Vector2 obstacleNormal)
    {
        // Reflect the movement direction based on the obstacle's normal vector
        return Vector2.Reflect(movementDirection, obstacleNormal).normalized;
    }
    private void OnDrawGizmos()
    {
        // Visualize the boundary in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(boundary.x + boundary.width / 2, boundary.y + boundary.height / 2, 0), 
                            new Vector3(boundary.width, boundary.height, 0));

        // Visualize the detection radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
