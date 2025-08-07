using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovementController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterInputBridge input;
    private CharacterController controller;

    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;

    [Header("Jump Settings")]
    public float jumpForce = 5f;
    public float gravity = -9.81f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isAutoRunning;

    private Vector2 lookInput = Vector2.zero;

    public void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
            lookInput = context.ReadValue<Vector2>();
    }

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleJump();
        HandleAutoRun();
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        Vector2 inputDir = input.MoveInput;
        float forwardInput = isAutoRunning ? 1f : inputDir.y;

        Vector3 move = Vector3.zero;

        if (!input.IsLeftClickHeld)
        {
            move += transform.forward * forwardInput;

            if (input.IsRightClickHeld)
            {
                move += transform.right * inputDir.x;
            }
        }

        controller.Move(move * moveSpeed * Time.deltaTime);

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        else
            velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector2 inputDir = input.MoveInput;

        if (!input.IsRightClickHeld && Mathf.Abs(inputDir.x) > 0.1f)
        {
            float turnAmount = inputDir.x * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, turnAmount);
        }

        if (input.IsRightClickHeld)
        {
            float mouseX = lookInput.x;
            if (Mathf.Abs(mouseX) > 0.01f)
            {
                float turnAmount = mouseX * rotationSpeed * Time.deltaTime * 0.1f;
                transform.Rotate(Vector3.up, turnAmount);
            }
        }
    }

    private void HandleJump()
    {
        if (isGrounded && input.ConsumeJumpPressed())
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    private void HandleAutoRun()
    {
        if (input.ConsumeAutoRunToggled())
        {
            isAutoRunning = !isAutoRunning;
        }

        if (input.MoveInput.y < -0.1f)
        {
            isAutoRunning = false;
        }
    }
}
