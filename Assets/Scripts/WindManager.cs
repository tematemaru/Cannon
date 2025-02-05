using UnityEngine;

public class WindManager : MonoBehaviour
{
    [Header("Настройки ветра")]
    [SerializeField] private Vector3 baseWindDirection = Vector3.forward;
    [SerializeField] private float baseWindStrength = 2f;
    [SerializeField] private float windVariation = 1f;
    [SerializeField] private float windChangeInterval = 3f;

    private Vector3 currentWind;
    private float timeSinceLastChange;

    public static Vector3 GlobalWind { get; private set; }

    void Start()
    {
        CalculateNewWind();
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (timeSinceLastChange >= windChangeInterval)
        {
            CalculateNewWind();
            timeSinceLastChange = 0f;
        }
    }

    void CalculateNewWind()
    {
        Vector3 variation = new Vector3(
            Random.Range(-windVariation, windVariation),
            0,
            Random.Range(-windVariation, windVariation)
        );

        currentWind = baseWindDirection.normalized * baseWindStrength + variation;
        GlobalWind = currentWind;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, currentWind * 2f);
        Gizmos.DrawWireSphere(transform.position + currentWind * 2f, 0.5f);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 40, 300, 20), $"Current Wind: {GlobalWind}");
    }
}