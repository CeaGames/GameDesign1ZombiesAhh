using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoorBarricade : MonoBehaviour
{
    public float hp;

    private float _radius = 2.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // Physics.OverlapSphere(transform.position, _radius);
    }

}
