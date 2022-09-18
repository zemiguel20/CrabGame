using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CrabController : MonoBehaviour
{
    public float speed;
    public Vector2 directionInput;

    public UnityEvent<Collision> collisionEvent;

    private Rigidbody rb;
    private BoxCollider coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        directionInput.Normalize();
        Vector3 direction = new Vector3(directionInput.x, 0.0f, directionInput.y);

        // If colliding horizontally, nullify horizontal direction
        // Allows vertical wall sliding
        Vector3 horizontalDir = new Vector3(direction.x, 0.0f, 0.0f);
        if (Physics.Raycast(rb.position, horizontalDir, coll.size.x))
        {
            direction.x = 0.0f;
        }

        // If colliding vertically, nullify vertical direction
        // Allows horizontal wall sliding
        Vector3 verticalDir = new Vector3(0.0f, 0.0f, direction.z);
        if (Physics.Raycast(rb.position, verticalDir, coll.size.z))
        {
            direction.z = 0.0f;
        }

        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionEvent.Invoke(collision);
    }
}
