using System.Collections;
using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    [Header("Active Skills (arrastrar ScriptableObjects)")]
    public SkillBase[] activeSkills = new SkillBase[6];

    [Header("Passive Skill")]
    public PassiveSkillBase passiveSkill;

    private CharacterBase character;

    private TargetSelector targetSelector;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
        targetSelector = GetComponent<TargetSelector>();

        if (character.autoAttack != null && activeSkills.Length > 0)
        {
            SkillAutoAttack auto = activeSkills[0] as SkillAutoAttack;
            if (auto != null)
            {
                character.autoAttack.SetAutoAttackSkill(auto);
            }
        }
        if (passiveSkill != null)
        {
            passiveSkill.ApplyPassiveEffect(character);
        }
        foreach (var skill in activeSkills)
        {
            if (skill != null)
            {
                skill.ResetCooldown();
            }
        }
    }

    public void UseSkill(int index, CharacterBase target = null)
    {
        if (index < 0 || index >= activeSkills.Length) return;

        SkillBase skill = activeSkills[index];
        if (skill == null || !skill.IsReady()) return;

        if (character.isStunned)
        {
            Debug.Log($"{character.characterName} está aturdido y no puede usar habilidades.");
            return;
        }

        if (character.isSilenced && !(skill is SkillAutoAttack))
        {
            Debug.Log($"{character.characterName} está silenciado y no puede usar habilidades activas.");
            return;
        }

        if (target == null && targetSelector != null && targetSelector.HasTarget)
            target = targetSelector.currentTarget;
        
        if (target == null) return;
        
        character.InterruptAutoAttack();
        character.StartCoroutine(ExecuteSkillWithAutoResume(skill, target));
    }


    private IEnumerator ExecuteSkillWithAutoResume(SkillBase skill, CharacterBase target)
    {
        yield return skill.Execute(character, target);

        character.ResumeAutoAttack(target); // REANUDA autoataque
    }

    public SkillBase GetSkill(int index)
    {
        if (index < 0 || index >= activeSkills.Length) return null;
        return activeSkills[index];
    }
}
