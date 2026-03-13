using UnityEngine;

public class health1 : MonoBehaviour
{
    public int defaultHealthPoint = 100;
    public int healthPoint;

    public System.Action onDead;
    public System.Action onHealthChanged;

    void Start()
    {
        healthPoint = defaultHealthPoint;
        onHealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        if (healthPoint <= 0) return;

        healthPoint -= damage;

        onHealthChanged?.Invoke();

        if (healthPoint <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        onDead?.Invoke();
    }
}