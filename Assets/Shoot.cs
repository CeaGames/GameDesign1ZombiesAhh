using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //make the list that holds items
    public List<string> items = new List<string>();

    //range of pickup
    public float range = 5f;

    //a layermask so you can only pickup items and not walls or zombies
    public LayerMask itemLayerMask;

    //a layermask to know when you are lookin at a doorframe that can be barricaded
    public LayerMask doorFrameLayerMask;

    //the first person camera needed to center the ray
    public Camera gunCamera;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }

    // shoot and if hit give the name in the console log, add it to your list and destroy the game object after that

    //it also makes you put planks on doorframes if you point at doorframes
    private void Shooting() 
    {
        RaycastHit pickup;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out pickup, range, itemLayerMask)) 
        {
            Debug.Log(pickup.transform.name);
            items.Add(pickup.transform.gameObject.name);
            pickup.collider.gameObject.SetActive(false);
        }

        RaycastHit barricade;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out barricade, range, doorFrameLayerMask))
        {
            if(barricade.transform != null)
            {
                barricade.collider.gameObject.GetComponent<DoorBarricade>().hp += barricade.collider.gameObject.GetComponent<DoorBarricade>().maxHp / 3;
            }
        }
    }
}
