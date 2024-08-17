using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float movementSpeed = 10f;

    [SerializeField]
    private int maxHealth = 100;

    [SerializeField]
    private HealthBar healthBar;

    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new(maxHealth, healthBar);
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Projectile"))
        {
            return;
        }

        float dealtDamage = other.GetComponent<BasicProjectile>().Damage;

        healthSystem.DealDamage(dealtDamage);

        if (healthSystem.Health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
