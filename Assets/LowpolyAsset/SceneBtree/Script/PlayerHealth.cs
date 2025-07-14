using UnityEngine;

public class PlayerHealth : HealthSystem
{
    public float regenRate = 5f;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating(nameof(RegenHealth), 2f, 1f);
    }

    void RegenHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += (int)regenRate;
            currentHealth = Mathf.Min(currentHealth, maxHealth);
            UpdateHealthUI();
        }
    }

    public override void Die()
    {
        base.Die();
        FindObjectOfType<GameManager>().GameOver();
    }
}