using System;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public Action OnPlayerDeath;

    [SerializeField]
    private HealthBar healthBar;

    [SerializeField]
    private int maxHealth = 500;

    private HealthSystem healthSystem;

    private void Start()
    {
        healthSystem = new(maxHealth, healthBar);
    }

    public void DealtDamage(float damageAmount)
    {
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
}
