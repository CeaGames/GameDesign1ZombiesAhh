using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementCC : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public Transform orientation;

    [Header("Gravity")]
    public float gravity = -9.81f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 movementDirection;
    private Vector3 velocity;

    private bool isGrounded;

    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();
        MyInput();
        MovePlayer();
        ApplyGravity();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        // Movement relative to orientation
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        movementDirection.Normalize();

        controller.Move(movementDirection * moveSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void GroundCheck()
    {
        Vector3 checkPosition = transform.position - new Vector3(0f, controller.height / 2f, 0f);
        isGrounded = Physics.CheckSphere(checkPosition, groundCheckDistance, groundMask);
    }
}

