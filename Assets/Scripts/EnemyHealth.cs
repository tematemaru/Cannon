using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("���������")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private int scoreValue = 50;

    private int currentHealth;
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    //[SerializeField] private Slider healthSlider;

    void UpdateHealthUI()
    {
       // healthSlider.value = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        isDead = true;

        // ������� ����������� �����
        GameManager.Instance.AddScore(scoreValue);

        // ������ �����������
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}