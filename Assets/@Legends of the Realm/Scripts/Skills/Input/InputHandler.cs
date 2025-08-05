using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private CharacterSkills skills;
    private InputTargetSelector inputTargetSelector;

    private void Awake()
    {
        skills = GetComponent<CharacterSkills>();
        inputTargetSelector = GetComponent<InputTargetSelector>();
    }
    public void OnUseSkill1(InputAction.CallbackContext context)
    {
        if (context.performed)
            skills.UseSkill(0, FindTarget());
    }

    public void OnUseSkill2(InputAction.CallbackContext context)
    {
        if (context.performed)
            skills.UseSkill(1, FindTarget());
    }
    public void OnUseSkill3(InputAction.CallbackContext context)
    {
        if (context.performed)
            skills.UseSkill(2, FindTarget());
    }
    public void OnUseSkill4(InputAction.CallbackContext context)
    {
        if (context.performed)
            skills.UseSkill(3, FindTarget());
    }
    public void OnUseSkill5(InputAction.CallbackContext context)
    {
        if (context.performed)
            skills.UseSkill(4, FindTarget());
    }
    public void OnUseSkill6(InputAction.CallbackContext context)
    {
        if (context.performed)
            skills.UseSkill(5, FindTarget());
    }
    public void OnSelectTarget(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputTargetSelector.SelectTargetUnderCursor();
        }
    }
    private CharacterBase FindTarget()
    {
        // Encuentra el primer enemigo en la escena
        CharacterBase[] all = FindObjectsOfType<CharacterBase>();
        CharacterBase self = GetComponent<CharacterBase>();

        foreach (var c in all)
        {
            if (c != self && self.IsEnemyTo(c) && !c.stats.IsDead)
                return c;
        }

        return null;
    }
}
