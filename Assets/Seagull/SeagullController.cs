using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class SeagullController : MonoBehaviour
{
    public float speed;
    public float maxAngle;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector2 target)
    {
        transform.LookAt(new Vector3(target.x, transform.position.y, target.y));
        transform.Rotate(transform.up, Random.Range(-maxAngle, maxAngle));
    }

    void FixedUpdate()
    {
        //rb.velocity = transform.forward * speed;
        rb.position += transform.forward * speed * Time.fixedDeltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Collided with player");

            GameMode gamemode = FindObjectOfType<GameMode>();
            if (gamemode != null) gamemode.EndGame(false);
        }
    }

    void OnTriggerExit(Collider other)
    {
        gameObject.SetActive(false);
    }
}
