using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    protected int currentHealth;

    public Slider healthSlider; 

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        if (healthSlider != null)
            healthSlider.maxValue = maxHealth;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthUI();

        if (currentHealth <= 0)
            Die();
    }

    protected virtual void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = currentHealth;
    }

    public virtual void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
    }
}