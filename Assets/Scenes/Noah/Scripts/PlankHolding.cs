using Unity.Mathematics;
using UnityEngine;

public class PlankHolding : MonoBehaviour
{
    public GameObject plank;
    public Shoot yeahThatOne;

    void Start()
    {
        
    }

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

        if (yeahThatOne.items.Count == 0)
        {
            plank.SetActive(false);
        }
        else
        {
            plank.SetActive(true);
        }


        
    }
}
