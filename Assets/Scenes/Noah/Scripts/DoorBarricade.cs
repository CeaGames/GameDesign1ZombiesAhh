using System.Linq;
using Unity.VisualScripting;
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

      if (hp < 0)
        {
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }

    }

}
