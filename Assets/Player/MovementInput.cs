using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private CharacterController charControl;

    public float speed;

    private Vector3 direction = Vector3.zero;

    void Update()
    {
        direction.x = Input.GetAxisRaw("HorizontalKeys");
        direction.z = Input.GetAxisRaw("VerticalKeys");
        direction.Normalize();
        charControl.SimpleMove(direction * speed);
    }
}
