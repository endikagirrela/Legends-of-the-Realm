using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    public SkillBase skill;
    public Image fillImage;

    private void Update()
    {
        if (skill == null || fillImage == null) return;
        float remaining = (skill.lastUsedTime + skill.cooldown) - Time.time;
        fillImage.fillAmount = Mathf.Clamp01(remaining / skill.cooldown);
    }
}
