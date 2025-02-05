using UnityEngine;

[RequireComponent(typeof(CannonAim))]
public class ShootController : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireForce = 30f;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float projectileLifetime = 5f;

    private CannonAim cannonAim;
    private float nextFireTime;

    public float ProjectileMass => projectilePrefab.GetComponent<Rigidbody>().mass;
    public float FireForce => fireForce;

    void Awake()
    {
        cannonAim = GetComponent<CannonAim>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            FireProjectile();
            nextFireTime = Time.time + fireRate;
        }
    }

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            cannonAim.FirePosition,
            Quaternion.LookRotation(cannonAim.FireDirection)
        );

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb) rb.AddForce(cannonAim.FireDirection * fireForce, ForceMode.Impulse);

        Destroy(projectile, projectileLifetime);
    }
}