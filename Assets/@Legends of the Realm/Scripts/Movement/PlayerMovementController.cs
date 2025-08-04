using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterBase))]
public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;
    public float rotationSpeed = 180f;
    public float gravity = -9.81f;

    [Header("References")]
    public Transform cameraTransform;
    public LayerMask targetLayerMask;
    public float targetMaxDistance = 30f;

    private CharacterController controller;
    private CharacterBase character;
    private Vector3 velocity;
    private Transform currentTarget;

    // Input cache
    private Vector2 moveInput = Vector2.zero;

    // Coroutine guard
    private Coroutine moveAndThenCoroutine;

    public Transform CurrentTarget => currentTarget;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        character = GetComponent<CharacterBase>();
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        ApplyMovement();
    }

    // Input System binding
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // x = turn, y = forward/back
        if (context.canceled)
            moveInput = Vector2.zero;
    }

    private void ApplyMovement()
    {
        float forwardInput = moveInput.y;
        float turnInput = moveInput.x;

        // Rotación manual (A/D)
        transform.Rotate(Vector3.up, turnInput * rotationSpeed * Time.deltaTime);

        // Movimiento adelante/atrás
        Vector3 move = transform.forward * forwardInput;
        if (move.sqrMagnitude > 0f)
        {
            move.Normalize();
            controller.Move(move * moveSpeed * Time.deltaTime);
        }

        // Gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
    }

    public void SetTarget(Transform newTarget) => currentTarget = newTarget;
    public void ClearTarget() => currentTarget = null;

    public bool IsTargetInAttackRange()
    {
        return IsTargetInAttackRange(currentTarget);
    }

    public bool IsTargetInAttackRange(Transform target)
    {
        if (target == null || character == null) return false;
        float dist = Vector3.Distance(transform.position, target.position);
        return dist <= character.Stats.attackRange;
    }

    /// <summary>
    /// Mueve hacia el objetivo hasta que esté en rango de ataque y luego ejecuta la acción callback.
    /// </summary>
    public void MoveToTargetAndThenExecute(Transform target, Action onReached)
    {
        if (moveAndThenCoroutine != null)
            StopCoroutine(moveAndThenCoroutine);
        moveAndThenCoroutine = StartCoroutine(MoveUntilInRange(target, onReached));
    }

    private IEnumerator MoveUntilInRange(Transform target, Action onReached)
    {
        while (target != null && !IsTargetInAttackRange(target))
        {
            // Avanza hacia el objetivo
            Vector3 direction = (target.position - transform.position);
            direction.y = 0f;
            if (direction.sqrMagnitude > 0.01f)
            {
                direction.Normalize();
                controller.Move(direction * moveSpeed * Time.deltaTime);

                // Rotación instantánea hacia el objetivo
                transform.rotation = Quaternion.LookRotation(direction);
            }

            // gravedad
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
            if (controller.isGrounded && velocity.y < 0)
                velocity.y = -2f;

            yield return null;
        }

        onReached?.Invoke();
    }
}
