public class HealthSystem
{
    public float Health { private set; get; }

    private readonly HealthBar healthBar;
    private readonly float initialHealth;

    public HealthSystem(int health, HealthBar healthBar)
    {
        Health = health;
        initialHealth = health;

        this.healthBar = healthBar;
    }

    public void DealDamage(float damage)
    {
        Health -= damage;

        if (Health < 0)
        {
            Health = 0;
        }

        healthBar.SetSize(Health / initialHealth);
    }
}
