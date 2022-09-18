using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class CharacterCollisionEvent : MonoBehaviour
{
    public UnityEvent<ControllerColliderHit> collisionEvent;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        collisionEvent.Invoke(hit);
    }
}
