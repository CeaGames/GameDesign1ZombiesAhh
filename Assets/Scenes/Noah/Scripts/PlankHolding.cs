using Unity.Mathematics;
using UnityEngine;

public class PlankHolding : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            transform.Rotate(0, 0, 45);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            transform.Rotate(0, 0, -45);
        }

        
    }
}
