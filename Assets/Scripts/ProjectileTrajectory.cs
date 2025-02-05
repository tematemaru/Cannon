using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ProjectileTrajectory : MonoBehaviour
{
    [Header("Trajectory Settings")]
    [SerializeField] private int pointsCount = 30;
    [SerializeField] private float timeStep = 0.1f;
    [SerializeField] private LayerMask collisionMask;

    private LineRenderer trajectoryLine;
    private CannonAim cannonAim;
    private ShootController shootController;
    private Vector3[] trajectoryPoints;

    void Start()
    {
        trajectoryLine = GetComponent<LineRenderer>();
        cannonAim = GetComponent<CannonAim>();
        shootController = GetComponent<ShootController>();
        trajectoryLine.positionCount = pointsCount;
        trajectoryPoints = new Vector3[pointsCount];
    }

    void Update()
    {
        SetVisible(Input.GetMouseButton(1)); // Показывать при зажатии ПКМ
        CalculateTrajectory();
        UpdateLineRenderer();
    }

    void CalculateTrajectory()
    {
        Vector3 velocity = cannonAim.FireDirection * shootController.FireForce / shootController.ProjectileMass;
        Vector3 position = cannonAim.FirePosition;

        for (int i = 0; i < pointsCount; i++)
        {
            trajectoryPoints[i] = position;

            if (Physics.Raycast(position, velocity, out RaycastHit hit, velocity.magnitude * timeStep, collisionMask))
            {
                FillRemainingPoints(i, hit.point);
                return;
            }

            position += velocity * timeStep;
            velocity += Physics.gravity * timeStep;
        }
    }

    void FillRemainingPoints(int startIndex, Vector3 hitPoint)
    {
        for (int i = startIndex; i < pointsCount; i++)
            trajectoryPoints[i] = hitPoint;
    }

    void UpdateLineRenderer()
    {
        trajectoryLine.SetPositions(trajectoryPoints);
    }

    public void SetVisible(bool visible) => trajectoryLine.enabled = visible;
}