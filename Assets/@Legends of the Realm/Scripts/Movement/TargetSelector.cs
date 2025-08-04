using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerMovementController))]
public class TargetSelector : MonoBehaviour
{
    public Camera cam;
    public LayerMask targetLayerMask;
    public float targetMaxDistance = 30f;

    private PlayerMovementController movementController;
    private CharacterBase character;

    void Awake()
    {
        if (cam == null)
            cam = Camera.main;

        movementController = GetComponent<PlayerMovementController>();
        character = GetComponent<CharacterBase>();
    }

    // Conecta esto al click izquierdo
    public void OnSelectTarget(InputAction.CallbackContext callback)
    {
        if (!callback.canceled) return;
        if (cam == null) return;

        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = cam.ScreenPointToRay(mousePos);
        if (!Physics.Raycast(ray, out var hit, targetMaxDistance, targetLayerMask))
            return;

        var hitTransform = hit.collider.transform;
        var potential = hitTransform.GetComponent<CharacterBase>();
        if (potential == null) return;

        // Si ya está seleccionado: segundo click
        if (movementController.CurrentTarget == hitTransform)
        {
            // Iniciar proceso de ataque: mover si hace falta y luego atacar
            StartAttackSequence(potential);
        }
        else
        {
            // Primer click: solo seleccionar, no girar ni atacar
            movementController.SetTarget(hitTransform);
            // Opcional: feedback visual de selección
        }
    }

    private void StartAttackSequence(CharacterBase target)
    {
        // Si ya está en rango, ataca directamente
        if (movementController.IsTargetInAttackRange())
        {
            FaceAndAttack(target);
        }
        else
        {
            // Mover hacia él hasta estar en rango, luego atacar
            movementController.MoveToTargetAndThenExecute(target.transform, () =>
            {
                FaceAndAttack(target);
            });
        }
    }

    private void FaceAndAttack(CharacterBase target)
    {
        movementController.SetTarget(target.transform); // para que gire hacia él en movement
        // Asegura que esté completamente orientado (podrías esperar un frame si quieres)
        // Ejecutar ataque: ejemplo con skill 0 o BasicAttack
        character.UseSkill(0, target);
        // o: character.BasicAttack(target);
    }

    // Derecho para limpiar
    public void OnClearTarget(InputAction.CallbackContext callback)
    {
        if (!callback.canceled) return;
        movementController.ClearTarget();
    }
}
