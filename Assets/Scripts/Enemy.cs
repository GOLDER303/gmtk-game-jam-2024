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

    private Rigidbody rb;

    public void Setup(Transform player)
    {
        this.player = player;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerHealthManager = player.gameObject.GetComponent<PlayerHealthManager>();

        healthSystem = new(maxHealth, healthBar);
    }

    private void Update()
    {
        if (shouldMoveTowardsThePlayer)
        {
            Vector3 vectorFromEnemyToPlayer = player.position - transform.position;

            if (vectorFromEnemyToPlayer.magnitude > .01f)
            {
                Vector3 direction = vectorFromEnemyToPlayer.normalized;

                rb.velocity = direction * movementSpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
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
