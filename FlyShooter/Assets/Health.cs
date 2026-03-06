using UnityEngine;
using System;                     // ← DÒNG NÀY RẤT QUAN TRỌNG

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint = 3;

    public System.Action onDead;   // ← DÒNG NÀY PHẢI CÓ

    protected int currentHealthPoint;

    protected virtual void Start()
    {
        currentHealthPoint = defaultHealthPoint;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealthPoint -= damage;
        if (currentHealthPoint <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
        Destroy(explosion, 1f);
        Destroy(gameObject);

        onDead?.Invoke();          // ← Gọi event khi chết
    }
}