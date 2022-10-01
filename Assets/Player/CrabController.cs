using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CrabController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private BoxCollider coll;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }

    void FixedUpdate()
    {
        Vector3 direction = ReadInputDirection();

        // If colliding horizontally, nullify horizontal direction
        // Allows vertical wall sliding
        Vector3 horizontalDir = new Vector3(direction.x, 0.0f, 0.0f);
        float horizontalLength = coll.size.x * 0.5f * transform.lossyScale.x + 0.1f;
        Debug.DrawRay(rb.position, horizontalDir.normalized * horizontalLength);
        if (Physics.Raycast(rb.position, horizontalDir, horizontalLength))
        {
            direction.x = 0.0f;
        }

        // If colliding vertically, nullify vertical direction
        // Allows horizontal wall sliding
        Vector3 verticalDir = new Vector3(0.0f, 0.0f, direction.z);
        float verticalLength = coll.size.z * 0.5f * transform.lossyScale.z + 0.1f;
        Debug.DrawRay(rb.position, verticalDir.normalized * verticalLength);
        if (Physics.Raycast(rb.position, verticalDir, verticalLength))
        {
            direction.z = 0.0f;
        }

        Vector3 newVelocity = speed * direction;
        newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
    }

    Vector3 ReadInputDirection()
    {
        Vector3 dir = Vector3.zero;
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        return dir.normalized;
    }
}
