using UnityEngine;
using UnityEngine.AI;

public class WeepingAngelZombie : MonoBehaviour
{
    public Transform playerCamera;            // Assign the player's camera
    public Transform target;                  // The destination the zombie walks toward (e.g., player or a location)
    public float viewAngle = 70f;             // Field of view angle in degrees
    public float detectionDistance = 30f;     // Max distance the zombie can be seen from

    private NavMeshAgent agent;
    private bool isSeen = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        agent.avoidancePriority = 99;
    }


    void Update()
    {
        if (IsVisibleToPlayer())
        {
            // Stop if seen
            agent.isStopped = true;
        }
        else
        {
            // Move if not seen
            agent.isStopped = false;
            if (target != null)
            {
                agent.SetDestination(target.position);
            }
        }
    }

    bool IsVisibleToPlayer()
    {
        Vector3 directionToZombie = transform.position - playerCamera.position;
        float distance = directionToZombie.magnitude;

        if (distance > detectionDistance)
            return false;

        float angle = Vector3.Angle(playerCamera.forward, directionToZombie);

        if (angle < viewAngle / 2f)
        {
            // Raycast to check line of sight
            Ray ray = new Ray(playerCamera.position, directionToZombie.normalized);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, detectionDistance))
            {
                if (hit.transform == transform)
                {
                    return true; // Zombie is in view and visible
                }
            }
        }

        return false;
    }
}
