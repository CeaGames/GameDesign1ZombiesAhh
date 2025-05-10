
using UnityEngine;

public class CameraInteract : MonoBehaviour
{
            Ray ray;
            RaycastHit hit;
    
    public LayerMask mask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 5f, Color.yellow);


        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 5f, mask))
            {
                Debug.Log("hit");

                foreach (RaycastHit child in transform)
                {
                    GetComponent<BoxCollider>().enabled = true;
                    GetComponent<MeshRenderer>().enabled = true;
                }

            }
        }

    }

}
