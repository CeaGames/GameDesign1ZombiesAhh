using UnityEngine;
using UnityEngine.AI;

public class CeAiBehaviourScript : MonoBehaviour
{
    [SerializeField] private Transform _playerT;
    [SerializeField] private NavMeshAgent _zobmieAgent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _zobmieAgent.SetDestination(_playerT.position);
    }
}
