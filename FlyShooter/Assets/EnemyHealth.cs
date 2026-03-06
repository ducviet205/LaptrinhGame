using UnityEngine;

public class EnemyHealth : Health
{
    public static int LivingEnemyCount;

    private void Awake() => LivingEnemyCount++;

    protected override void Die()
    {
        LivingEnemyCount--;
        base.Die();
    }
}