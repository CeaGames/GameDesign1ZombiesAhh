using UnityEngine;

public class PlayerLookCopy : MonoBehaviour
{
    //mouse sensitivity
    public float MouseSensX;
    public float MouseSensY;

    public Transform orientation;

    public int startRotation;

    //camera rotation
    float xRotation;
    float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //add an invisible cursor that is locked in the middle of your screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        yRotation = startRotation;

        if (SettingsCarrier.mouseSensetivity != 0)
        {
            MouseSensX = SettingsCarrier.mouseSensetivity;
            MouseSensY = SettingsCarrier.mouseSensetivity;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SettingsCarrier.mouseSensetivity != 0)
        {
            MouseSensX = SettingsCarrier.mouseSensetivity;
            MouseSensY = SettingsCarrier.mouseSensetivity;
        }

        //mouse input
        float mouseX = Input.GetAxisRaw ("Mouse X") * Time.deltaTime * MouseSensX;
        float mouseY = Input.GetAxisRaw ("Mouse Y") * Time.deltaTime * MouseSensY;

        yRotation += mouseX;
        xRotation -= mouseY;

        //put a limit how far you can look down or up
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //rotate the camera on both axis
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        //rotate the player itself around the y-axis
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
