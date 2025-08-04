using UnityEngine;

public class TooltipProvider : MonoBehaviour
{
    public SkillBase skill;
    // Hook this up to UI on hover to display skill.skillName and skill.description
    public string GetTooltipText()
    {
        return $"{skill.skillName}\n{skill.description}";
    }
}
