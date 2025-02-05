using UnityEngine;

public class ProjectileWindEffect : MonoBehaviour
{
    [Header("Настройки ветрового воздействия")]
    [SerializeField] private float windDragCoefficient = 0.1f;
    [SerializeField] private float crossSectionArea = 0.5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyWindForce();
    }

    void ApplyWindForce()
    {
        if (rb == null) return;

        // Рассчитываем силу ветра по формуле: F = 0.5 * ρ * v² * A * Cd
        float airDensity = 1.225f; // Плотность воздуха
        Vector3 windVelocity = WindManager.GlobalWind;
        float windSpeed = windVelocity.magnitude;

        Vector3 windForce = 0.5f * airDensity *
                          Mathf.Pow(windSpeed, 2) *
                          crossSectionArea *
                          windDragCoefficient *
                          windVelocity.normalized;

        rb.AddForce(windForce, ForceMode.Force);
    }
}