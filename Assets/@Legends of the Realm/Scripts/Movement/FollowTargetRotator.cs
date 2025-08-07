using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class FollowTargetRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    public float verticalSensitivity = 1.5f;
    public float horizontalSensitivity = 100f;
    public float minAngle = -40f;
    public float maxAngle = 60f;

    [Header("Zoom Settings")]
    [SerializeField] private CinemachineThirdPersonFollow thirdPersonFollow;
    public float zoomSpeed = 5f;
    public float minZoom = 2f;
    public float maxZoom = 10f;
    private float zoomDelta;

    [Header("Dependencies")]
    [SerializeField] private CharacterInputBridge inputBridge;

    private float currentXRotation = 10f;
    private float currentYRotation = 0f;
    private Vector2 lookInput = Vector2.zero;

    public void OnLookInput(InputAction.CallbackContext context)
    {
        if (context.performed || context.canceled)
            lookInput = context.ReadValue<Vector2>();
    }

    public void OnZoom(InputAction.CallbackContext context)
    {
        zoomDelta = context.ReadValue<float>();
    }

    private void LateUpdate()
    {
        if (inputBridge == null) return;

        bool left = inputBridge.IsLeftClickHeld;
        bool right = inputBridge.IsRightClickHeld;

        // Solo rotar cámara si se mantiene clic izquierdo
        if (left && !right)
        {
            currentYRotation += lookInput.x * horizontalSensitivity * Time.deltaTime;
            currentXRotation -= lookInput.y * verticalSensitivity * Time.deltaTime;
            currentXRotation = Mathf.Clamp(currentXRotation, minAngle, maxAngle);
        }
        // Si no se pulsa ningún botón del mouse, cámara vuelve detrás del personaje
        else if (!left && !right)
        {
            var flatForward = inputBridge.transform.forward;
            flatForward.y = 0;
            if (flatForward != Vector3.zero)
            {
                currentYRotation = Quaternion.LookRotation(flatForward).eulerAngles.y;
            }
        }
        // Con clic derecho, cámara sigue al personaje, pero permite vertical look
        else if (right)
        {
            currentXRotation -= lookInput.y * verticalSensitivity * Time.deltaTime;
            currentXRotation = Mathf.Clamp(currentXRotation, minAngle, maxAngle);
            currentYRotation = inputBridge.transform.eulerAngles.y;
        }

        transform.localRotation = Quaternion.Euler(currentXRotation, currentYRotation, 0f);

        // Zoom
        if (Mathf.Abs(zoomDelta) > 0.01f && thirdPersonFollow != null)
        {
            float newZoom = thirdPersonFollow.CameraDistance - zoomDelta * zoomSpeed * Time.deltaTime;
            thirdPersonFollow.CameraDistance = Mathf.Clamp(newZoom, minZoom, maxZoom);
        }
    }
}
