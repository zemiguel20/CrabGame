using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SeagullController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    public void SetDirection(Vector2 direction)
    {
        Vector3 dir3D = new Vector3(direction.x, transform.position.y, direction.y);
        transform.LookAt(dir3D);
    }
}
