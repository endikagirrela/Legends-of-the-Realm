using UnityEngine;
using UnityEngine.InputSystem;

public class InputTargetSelector : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask targetLayer;
    private TargetSelector selector;

    private void Awake()
    {
        mainCamera = Camera.main;
        selector = GetComponent<TargetSelector>();
    }

    public void SelectTargetUnderCursor()
    {
        Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, targetLayer))
        {
            CharacterBase cb = hit.collider.GetComponentInParent<CharacterBase>();
            if (cb != null)
            {
                selector.SetTarget(cb);
            }
        }
    }
}
