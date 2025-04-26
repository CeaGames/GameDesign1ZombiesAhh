using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoorBarricade : MonoBehaviour
{
    public GameObject door;

    public float hp;

    private float _radius = 2.5f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

      if (hp < 0)
        {
            door.SetActive(false);
        }

    }

}
