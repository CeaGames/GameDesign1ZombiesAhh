using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movespeed;

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

        rb.AddForce(MovementDirection.normalized * movespeed * 10f, ForceMode.Force);
    }
}
