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
    private GameManager gameManager;
    private Coroutine damageDealingCoroutine;

    public void Setup(Transform player, GameManager gameManager)
    {
        this.player = player;
        this.gameManager = gameManager;

        this.gameManager.OnGameWon += HandleDeath;
    }

    void OnDisable()
    {
        gameManager.OnGameWon -= HandleDeath;
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

            if (vectorFromEnemyToPlayer.magnitude > .7f)
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

            if (damageDealingCoroutine == null)
            {
                damageDealingCoroutine = StartCoroutine(DamageDealingCoroutine());
            }
        }
    }

    private IEnumerator DamageDealingCoroutine()
    {
        while (true)
        {
            playerHealthManager.DealtDamage(damage);
            yield return new WaitForSeconds(timeBetweenDealingDamage);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            shouldMoveTowardsThePlayer = true;

            if (damageDealingCoroutine != null)
            {
                StopCoroutine(damageDealingCoroutine);
                damageDealingCoroutine = null;
            }
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
        if (gameObject)
        {
            Destroy(gameObject);
        }
    }
}
