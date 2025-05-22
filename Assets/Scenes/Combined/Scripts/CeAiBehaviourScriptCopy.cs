using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CeAiBehaviourScriptCopy : MonoBehaviour
{
    [SerializeField] private Transform _playerT;
    [SerializeField] private NavMeshAgent _zobmieAgent;

    public List<Transform> _doors = new List<Transform>();
    public List<Transform> _totalDoors = new List<Transform>();

    private Transform _nearestDoor = null;
    float _shortestDoorDistance = float.MaxValue;

    private float timer = 10;

    void Start()
    {

    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(timer < 0)
        {
            if (IsPathReachable(_playerT.position))
            {
                _zobmieAgent.SetDestination(_playerT.position);
            }
        }
    }



    bool IsPathReachable(Vector3 targetPosition)
    {
        NavMeshPath path = new NavMeshPath();
        _zobmieAgent.CalculatePath(targetPosition, path);
        return path.status == NavMeshPathStatus.PathComplete;
    }

    bool IsDoorReachable(Vector3 doorPosition)
    {
        float checkRadius = 1f; // You can adjust this value
        int checkPoints = 8; // Number of directions to test

        for (int i = 0; i < checkPoints; i++)
        {
            float angle = i * Mathf.PI * 2f / checkPoints;
            Vector3 offset = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * checkRadius;
            Vector3 checkPos = doorPosition + offset;

            NavMeshPath path = new NavMeshPath();
            _zobmieAgent.CalculatePath(checkPos, path);

            if (path.status == NavMeshPathStatus.PathComplete)
                return true;
        }

        return false;
    }

    void GetNearestDoor()
    {
        _shortestDoorDistance = float.MaxValue;
        _nearestDoor = null;

        // Remove inactive or null doors
        _doors.RemoveAll(door => door == null || !door.gameObject.activeSelf);

        foreach (Transform door in _doors)
        {
            float distance = Vector3.Distance(transform.position, door.position);

            if (distance < _shortestDoorDistance && IsDoorReachable(door.position))
            {
                _shortestDoorDistance = distance;
                _nearestDoor = door;
            }
        }
    }

}
