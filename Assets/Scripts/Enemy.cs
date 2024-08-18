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

    [SerializeField]
    private float damage = 10f;

    [SerializeField]
    private float timeBetweenDealingDamage = 1f;

    private float timeColliding;
    private bool shouldMoveTowardsThePlayer = true;

    private PlayerHealthManager playerHealthManager;

    private HealthSystem healthSystem;

    private void Start()
    {
        playerHealthManager = player.gameObject.GetComponent<PlayerHealthManager>();

        healthSystem = new(maxHealth, healthBar);
    }

    void Update()
    {
        if (shouldMoveTowardsThePlayer)
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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMoveTowardsThePlayer = false;

            timeColliding = 0f;

            playerHealthManager.DealtDamage(damage);
        }
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (timeColliding <= timeBetweenDealingDamage)
            {
                timeColliding += Time.deltaTime;
            }
            else
            {
                playerHealthManager.DealtDamage(damage);
                timeColliding = 0f;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMoveTowardsThePlayer = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            float dealtDamage = other.GetComponent<BasicProjectile>().Damage;

            healthSystem.DealDamage(dealtDamage);

            if (healthSystem.Health <= 0)
            {
                HandleDeath();
            }
        }

        if (other.CompareTag("DamagingProjectile"))
        {
            float dealtDamage = other.GetComponent<DamagingProjectile>().Damage;

            healthSystem.DealDamage(dealtDamage);

            if (healthSystem.Health <= 0)
            {
                HandleDeath();
            }
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
