using UnityEngine;

public class CharacterSkills : MonoBehaviour
{
    [Header("Active Skills (arrastrar ScriptableObjects)")]
    public SkillBase[] activeSkills = new SkillBase[6];

    [Header("Passive Skill")]
    public PassiveSkillBase passiveSkill;

    private CharacterBase character;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();

        // Aplicar habilidad pasiva al iniciar
        if (passiveSkill != null)
        {
            passiveSkill.ApplyPassiveEffect(character);
        }
    }

    public void UseSkill(int index, CharacterBase target)
    {
        if (index < 0 || index >= activeSkills.Length) return;

        SkillBase skill = activeSkills[index];
        if (skill != null && skill.IsReady())
        {
            character.StartCoroutine(skill.Execute(character, target));
        }
    }

    public SkillBase GetSkill(int index)
    {
        if (index < 0 || index >= activeSkills.Length) return null;
        return activeSkills[index];
    }
}
