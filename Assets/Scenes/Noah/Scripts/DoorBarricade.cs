using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DoorBarricade : MonoBehaviour
{
    public float hp;

<<<<<<< Updated upstream
=======
    private float _radius = 2.5f;
>>>>>>> Stashed changes

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< Updated upstream
      if (hp < 0)
        {
            Destroy(gameObject);
        }
=======
       // Physics.OverlapSphere(transform.position, _radius);
>>>>>>> Stashed changes
    }

}
