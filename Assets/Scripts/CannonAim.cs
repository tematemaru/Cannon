using UnityEngine;

public class CannonAim : MonoBehaviour
{
    [Header("Aiming Settings")]
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float maxVerticalAngle = 45f;
    [SerializeField] private float minVerticalAngle = -10f;
    [SerializeField] private float aimDistance = 100f;

    private Camera mainCamera;
    private Vector3 targetDirection;
    [SerializeField] private Transform firePoint;

    public Vector3 FireDirection => targetDirection.normalized;
    public Vector3 FirePosition => firePoint.position;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        UpdateAim();
    }

    void UpdateAim()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane aimPlane = new Plane(Vector3.up, transform.position.y);
        float enter = 0f;

        Vector3 targetPoint = ray.origin + ray.direction * aimDistance;

        if (aimPlane.Raycast(ray, out enter))
            targetPoint = ray.GetPoint(enter);

        targetDirection = targetPoint - firePoint.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Vector3 eulerRotation = targetRotation.eulerAngles;

        eulerRotation.x = ClampAngle(eulerRotation.x, minVerticalAngle, maxVerticalAngle);
        eulerRotation.z = 0;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.Euler(eulerRotation),
            rotationSpeed * Time.deltaTime
        );
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f) angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}