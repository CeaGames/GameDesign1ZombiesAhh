using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage = 10f;

    public float range = 100f;

    public Camera gunCamera;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shooting();
        }
    }
    private void Shooting() 
    {
        RaycastHit hit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out hit, range)) ;
        {
            Debug.Log(hit.transform.name);
        }
    }
}
