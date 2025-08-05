using UnityEngine;

public class AutoAttackController : MonoBehaviour
{
    private CharacterBase character;
    private Coroutine currentLoop;
    private bool isAttacking = false;
    private SkillAutoAttack autoAttackSkill;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    public void SetAutoAttackSkill(SkillAutoAttack skill)
    {
        autoAttackSkill = skill;
    }

    public void StartAutoAttack(CharacterBase target)
    {
        if (isAttacking)
        {
            StopAutoAttack();
            return;
        }

        if (autoAttackSkill == null || target == null) return;

        currentLoop = StartCoroutine(autoAttackSkill.AutoAttackLoop(character, target));
        isAttacking = true;
    }

    public void PauseAutoAttack()
    {
        if (currentLoop != null)
        {
            StopCoroutine(currentLoop);
            currentLoop = null;
            isAttacking = false;
        }
    }

    public void ResumeAutoAttack(CharacterBase target)
    {
        if (!isAttacking && autoAttackSkill != null && target != null)
        {
            currentLoop = StartCoroutine(autoAttackSkill.AutoAttackLoop(character, target));
            isAttacking = true;
        }
    }

    public void StopAutoAttack()
    {
        PauseAutoAttack();
    }
}
