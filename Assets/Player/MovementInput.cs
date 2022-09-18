using UnityEngine;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private CrabController controller;

    void Update()
    {
        Vector2 direction = new(
            Input.GetAxisRaw("HorizontalKeys"),
            Input.GetAxisRaw("VerticalKeys")
            );
        direction.Normalize();

        controller.directionInput = direction;
    }
}
