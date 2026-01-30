using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject explosionPrefab; // <-- field này

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Die();
        }
    }

    private void Die()
    {
        // Tạo vụ nổ tại vị trí enemy
        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            // Tự hủy sau 1 giây (thời gian animation)
            Destroy(explosion, 1f);
        }

        Destroy(gameObject); // Xóa enemy
    }
}