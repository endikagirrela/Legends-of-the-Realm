using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Passive Skills/RoyalGuard/Last Bastion")]
public class PassiveLastBastion : PassiveSkillBase
{
    public float hpThresholdPercent = 0.3f;
    public float healPercentPerSecond = 0.01f;
    public float duration = 10f;
    public float cooldown = 90f;

    private float lastTriggerTime = -999f;

    public override void ApplyPassiveEffect(CharacterBase character)
    {
        // Se asigna directamente desde RoyalGuard
    }

    public void TryTrigger(CharacterBase character)
    {
        if (Time.time < lastTriggerTime + cooldown)
            return;

        float hpPercent = character.stats.currentHP / character.stats.maxHP.TotalValue;
        if (hpPercent <= hpThresholdPercent)
        {
            lastTriggerTime = Time.time;
            character.StartCoroutine(HealOverTime(character));
        }
    }

    private IEnumerator HealOverTime(CharacterBase character)
    {
        float timePassed = 0f;
        float tick = 1f;

        while (timePassed < duration)
        {
            float amount = character.stats.maxHP.TotalValue * healPercentPerSecond;
            character.Heal(amount);
            yield return new WaitForSeconds(tick);
            timePassed += tick;
        }
    }
}
