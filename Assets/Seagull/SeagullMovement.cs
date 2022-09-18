using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeagullMovement : MonoBehaviour
{
    public float speed;

    private Vector3 direction = Vector3.zero;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    public void SetDirection(Transform player)
    {
        Vector3 pos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(pos);
    }
}
