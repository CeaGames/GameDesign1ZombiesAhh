using UnityEngine;
using UnityEngine.AI;

public class CeAiBehaviourScript : MonoBehaviour
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

        // Try pathing to player
        if (IsPathReachable(_playerT.position))
        {
            Debug.Log("PlayerIsReachable");
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
        float checkRadius = 2f; // You can adjust this value
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
        if (_doors.Length <= _doorCounter)
        {
            _doorCounter = 0;
            Debug.Log("reset");
        }

        float dist = Vector3.Distance(transform.position, _doors[_doorCounter].position);

        if (_doors.Length >= _doorCounter && dist < _shortestDoorDistance && _doors[_doorCounter].gameObject.activeSelf && IsDoorReachable(_doors[_doorCounter].position))
        {
                Debug.Log("doorFound");
                _shortestDoorDistance = dist;
                _nearestDoor = _doors[_doorCounter];
                _doorCounter++;
        }
        else if(_doors.Length >= _doorCounter)
        {
            _doorCounter++;
            //Debug.Log(_doorCounter);
        }
        //Debug.Log(_nearestDoor.name);
    }
}
