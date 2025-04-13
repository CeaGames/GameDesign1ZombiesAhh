using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Transform CameraMovement;

    // Update is called once per frame
    void Update()
    {
        transform.position = CameraMovement.position;
    }
}
