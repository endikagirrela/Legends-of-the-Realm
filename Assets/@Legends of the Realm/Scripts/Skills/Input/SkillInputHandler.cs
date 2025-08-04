using UnityEngine;
using UnityEngine.InputSystem;

public class SkillInputHandler : MonoBehaviour
{
    public CharacterSkills skills;

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

    private CharacterBase FindTarget()
    {
        // Aquí pones tu sistema real de targeting
        return null;
    }
}
