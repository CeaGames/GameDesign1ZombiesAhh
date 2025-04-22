using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoorBarricade : MonoBehaviour
{
    public float hp;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (hp < 0)
        {
            Destroy(gameObject);
        }
    }

}
