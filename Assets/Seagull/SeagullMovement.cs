using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullMovement : MonoBehaviour
{
    public float speed;

    private Vector3 direction = Vector3.zero;

    void Update()
    {
        transform.position += transform.forward * speed;
    }

    public void SetDirection(Transform player)
    {
        // TODO: implement
    }
}
