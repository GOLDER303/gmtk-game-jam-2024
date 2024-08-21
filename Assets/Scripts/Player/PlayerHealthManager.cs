using System;
using System.Collections;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public Action OnPlayerDeath;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private int maxHealth = 500;

    [SerializeField]
    private Material hitFlashMaterial;

    [SerializeField]
    private float flashEffectDuration = .1f;

    private SpriteRenderer playerSpriteRenderer;
    private Material defaultMaterial;
    private Coroutine flashEffectCoroutine;

    private HealthSystem healthSystem;

    private void Start()
    {
        playerSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        defaultMaterial = playerSpriteRenderer.material;

        healthSystem = new(maxHealth, healthBar);
    }

    public void DealtDamage(float damageAmount)
    {
        PlayFlashEffect();

        healthSystem.DealDamage(damageAmount);

        if (healthSystem.Health <= 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        OnPlayerDeath?.Invoke();
        Debug.Log("game over");
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
        playerSpriteRenderer.material = hitFlashMaterial;

        yield return new WaitForSeconds(flashEffectDuration);

        playerSpriteRenderer.material = defaultMaterial;

        flashEffectCoroutine = null;
    }
}
