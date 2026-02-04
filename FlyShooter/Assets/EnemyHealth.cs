using UnityEngine;



public class EnemyHealth : Health
{
    protected override void Die()
    {
        base.Die();
        Debug.Log("Enemy died");
    }
}
