using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Настройки")]
    [SerializeField] private int damage = 50;
    [SerializeField] private GameObject impactEffect;

    void OnCollisionEnter(Collision collision)
    {
        // Наносим урон врагам
        EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // Спавним эффект попадания
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}