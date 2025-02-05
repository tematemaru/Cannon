using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("��������� ��������")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float stoppingDistance = 5f;

    private Transform player;
    private bool isActive = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isActive || player == null) return;

        // ������� � ������
        Vector3 direction = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );

        // �������� � ������
        if (Vector3.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
    }

    public void StopMovement()
    {
        isActive = false;
    }
}