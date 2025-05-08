using UnityEngine;
using UnityEngine.AI;

public class CeAiBehaviourScriptCopy : MonoBehaviour
{
    [SerializeField] private Transform _playerT;
    [SerializeField] private NavMeshAgent _zobmieAgent;
    [SerializeField] private Transform[] _doors;

    private Transform _nearestDoor = null;
    private int _doorCounter = 0;
    float _shortestDoorDistance = float.MaxValue;

    void Start()
    {
        GameObject[] doorObjects = GameObject.FindGameObjectsWithTag("Door");
        _doors = new Transform[doorObjects.Length];

        Debug.Log(doorObjects.Length);
        for (int i = 0; i < doorObjects.Length; i++)
        {
            _doors[i] = doorObjects[i].transform;
        }
    }

    void Update()
    {
        // Clean up if door was destroyed
        if (_nearestDoor == null || !_nearestDoor.gameObject.activeSelf)
        {
            _nearestDoor = null;
            _shortestDoorDistance = float.MaxValue;
        }

        // Try pathing to player
        if (IsPathReachable(_playerT.position))
        {
            _zobmieAgent.SetDestination(_playerT.position);
        }
        else
        {
            // Find nearest door
            GetNearestDoor();

            if (_nearestDoor != null)
            {
                _zobmieAgent.SetDestination(_nearestDoor.position);
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

        foreach (Transform door in _doors)
        {
            if (door == null || !door.gameObject.activeSelf) continue;

            float distance = Vector3.Distance(transform.position, door.position);

            if (distance < _shortestDoorDistance && IsDoorReachable(door.position))
            {
                _shortestDoorDistance = distance;
                _nearestDoor = door;
            }
        }
    }

}
