using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float movementSpeed = 10f;

    void Update()
    {
        Vector3 newPosition = Vector3.MoveTowards(
            transform.position,
            player.position,
            movementSpeed * Time.deltaTime
        );

        newPosition.y = transform.position.y;

        transform.position = newPosition;
    }
}
