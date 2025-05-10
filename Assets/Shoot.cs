using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //make the list that holds items
    public List<GameObject> items = new List<GameObject>();

    //range of pickup
    public float range = 5f;

    //a layermask so you can only pickup items and not walls or zombies
    public LayerMask layerMask;

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
    private void Shooting() 
    {
        RaycastHit hit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out hit, range, layerMask)) 
        {
            Debug.Log(hit.transform.name);
            items.Add(hit.transform.gameObject);
            hit.collider.gameObject.SetActive(false);
        }
    }
}
