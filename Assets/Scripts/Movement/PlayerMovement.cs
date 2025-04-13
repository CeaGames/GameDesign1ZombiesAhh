using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    public float moveSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 MovementDirection;

    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
        SpeedControl();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //movement direction
        MovementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(MovementDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl() 
    {
        Vector3 Speed = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        //if you are faster than Speed
        if (Speed.magnitude > moveSpeed) 
        {
            //calculate maxspeed
            Vector3 maxSpeed = Speed.normalized * moveSpeed;
            //apply maxspeed
            rb.linearVelocity = new Vector3(maxSpeed.x, rb.linearVelocity.y, maxSpeed.z);
        }
    }
}
