using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SeagullController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    [SerializeField] private MeshRenderer meshRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!meshRenderer.isVisible)
            gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    public void SetDirection(Vector2 direction)
    {
        Vector3 dir3D = new Vector3(direction.x, transform.position.y, direction.y);
        transform.LookAt(dir3D);
    }
}
