using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
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
        Debug.Log("game over");
    }
}
