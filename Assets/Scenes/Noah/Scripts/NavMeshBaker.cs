using UnityEngine;
using Unity.AI.Navigation;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface navMesh;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BakeNavMesh();
    }

    public void BakeNavMesh()
    {
        navMesh.BuildNavMesh();
    }
}
