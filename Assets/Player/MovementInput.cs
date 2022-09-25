using UnityEngine;
using UnityEngine.InputSystem;

public class MovementInput : MonoBehaviour
{
    [SerializeField] private CrabController controller;

    public void OnMove(InputAction.CallbackContext context)
    {
        controller.directionInput = context.ReadValue<Vector2>();
    }
}
