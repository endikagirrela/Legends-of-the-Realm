using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInputBridge : MonoBehaviour
{
    [Header("Input State")]
    public Vector2 MoveInput { get; private set; }
    public bool IsRightClickHeld { get; private set; }
    public bool IsLeftClickHeld { get; private set; }

    private bool jumpPressed = false;
    private bool autoRunToggled = false;

    public bool ConsumeJumpPressed()
    {
        bool value = jumpPressed;
        jumpPressed = false;
        return value;
    }

    public bool ConsumeAutoRunToggled()
    {
        bool value = autoRunToggled;
        autoRunToggled = false;
        return value;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
            MoveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
            jumpPressed = true;
    }

    public void OnAutoRun(InputAction.CallbackContext context)
    {
        if (context.performed)
            autoRunToggled = true;
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        IsRightClickHeld = context.phase == InputActionPhase.Performed;
    }

    public void OnLeftClick(InputAction.CallbackContext context)
    {
        IsLeftClickHeld = context.phase == InputActionPhase.Performed;
    }
}
