using System.Collections;
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

    [SerializeField]
    private Material hitFlashMaterial;

    [SerializeField]
    private float flashEffectDuration = .1f;

    private float timeColliding;
    private bool shouldMoveTowardsThePlayer = true;
    private Coroutine flashEffectCoroutine;
    private Material defaultMaterial;

    private PlayerHealthManager playerHealthManager;
    private HealthSystem healthSystem;
    private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem hitParticleSystem;

    public void Setup(Transform player)
    {
        this.player = player;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        hitParticleSystem = GetComponentInChildren<ParticleSystem>();

        defaultMaterial = spriteRenderer.material;

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
        if (!other.CompareTag("Projectile") && !other.CompareTag("DamagingProjectile"))
        {
            return;
        }

        float dealtDamage = other.GetComponent<Projectile>().Damage;
        HandleHit(dealtDamage);
    }

    private void HandleHit(float deltDamage)
    {
        PlayFlashEffect();
        hitParticleSystem.Play();

        healthSystem.DealDamage(deltDamage);

        if (healthSystem.Health <= 0)
        {
            HandleDeath();
        }
    }

    private void PlayFlashEffect()
    {
        if (flashEffectCoroutine != null)
        {
            StopCoroutine(flashEffectCoroutine);
        }

        flashEffectCoroutine = StartCoroutine(FlashEffectCoroutine());
    }

    private IEnumerator FlashEffectCoroutine()
    {
        spriteRenderer.material = hitFlashMaterial;

        yield return new WaitForSeconds(flashEffectDuration);

        spriteRenderer.material = defaultMaterial;

        flashEffectCoroutine = null;
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
