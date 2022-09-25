using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class CrabController : MonoBehaviour
{
    [NonSerialized] public float speed;
    [NonSerialized] public Vector2 directionInput;

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
        Debug.DrawRay(rb.position, horizontalDir * coll.size.x * 0.5f);
        if (Physics.Raycast(rb.position, horizontalDir, coll.size.x * 0.5f))
        {
            direction.x = 0.0f;
        }

        // If colliding vertically, nullify vertical direction
        // Allows horizontal wall sliding
        Vector3 verticalDir = new Vector3(0.0f, 0.0f, direction.z);
        Debug.DrawRay(rb.position, verticalDir * coll.size.z * 0.5f);
        if (Physics.Raycast(rb.position, verticalDir, coll.size.z * 0.5f))
        {
            direction.z = 0.0f;
        }

        rb.position += speed * Time.fixedDeltaTime * direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionEvent.Invoke(collision);
    }
}
