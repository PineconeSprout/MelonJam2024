using UnityEngine;

public class Wander : MonoBehaviour
{
    public float speed = 50f; // Movement speed
    public float roamingTime = 3f; // Time before changing direction
    public float detectionRadius = 2f; // How far to check for obstacles (e.g. table)
    public LayerMask obstacleLayer; // Define obstacles (the table in this case)

    private Vector2 movementDirection;
    private float timer;
    private Draggable draggableScript;

    private void Awake(){
        draggableScript=GetComponent<Draggable>();
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

            // Move the character
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
        transform.Translate(movementDirection * speed * Time.deltaTime);
    }
    
}
