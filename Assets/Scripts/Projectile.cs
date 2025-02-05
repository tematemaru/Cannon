using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private int damage = 50;
    [SerializeField] private GameObject impactEffect;

    void OnCollisionEnter(Collision collision)
    {
        // ������� ���� ������
        EnemyHealth enemy = collision.collider.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // ������� ������ ���������
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}