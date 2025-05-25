using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CeAiBehaviourScriptCopy : MonoBehaviour
{
    [SerializeField] private Transform _playerT;
    [SerializeField] private NavMeshAgent _zobmieAgent;

    [SerializeField] private float offsetUpdateInterval = 4f;
    [SerializeField] private float offsetRange = 5f;
    [SerializeField] private float directChaseDistance = 3f;

    private Vector3 currentOffset;
    private float offsetTimer;

    private float timeBeforeStart = 10;

    void Start()
    {
        UpdateOffset(); // Initialize first offset
        offsetTimer = offsetUpdateInterval;
    }

    void Update()
    {
        timeBeforeStart -= Time.deltaTime;

        // Update the random offset every few seconds
        if(timeBeforeStart <= 0)
        {
            offsetTimer -= Time.deltaTime;

            if (offsetTimer <= 0)
            {
                UpdateOffset();
                offsetTimer = offsetUpdateInterval;
            }

            Vector3 destination;

            // If close enough to the player, use direct path
            float distanceToPlayer = Vector3.Distance(transform.position, _playerT.position);
            if (distanceToPlayer <= directChaseDistance)
            {
                destination = _playerT.position;
            }
            else
            {
                destination = _playerT.position + currentOffset;
            }

            if (IsPathReachable(destination))
            {
                _zobmieAgent.SetDestination(destination);
            }
        }
    }

    void UpdateOffset()
    {
        currentOffset = new Vector3(Random.Range(-offsetRange, offsetRange), 0, Random.Range(-offsetRange, offsetRange));
    }

    bool IsPathReachable(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        _zobmieAgent.CalculatePath(targetPosition, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }
}
