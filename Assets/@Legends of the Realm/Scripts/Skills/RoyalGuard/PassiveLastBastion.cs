using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "PassiveSkills/LastBastion")]
public class PassiveLastBastion : PassiveSkillBase
{
    public float defenseBonusPercent = 0.25f;
    public float lifeRegenPercentPerSecond = 0.02f;
    public float duration = 5f;
    public float internalCooldown = 60f;

    private bool onCooldown = false;

    public override void ApplyPassiveEffect(CharacterBase character)
    {
        character.StartCoroutine(MonitorHealth(character));
    }

    private IEnumerator MonitorHealth(CharacterBase character)
    {
        while (true)
        {
            yield return null;
            if (!onCooldown && character.Stats.currentHP / character.Stats.maxHP <= 0.3f)
            {
                character.StartCoroutine(ApplyBuff(character));
                onCooldown = true;
                yield return new WaitForSeconds(internalCooldown);
                onCooldown = false;
            }
        }
    }

    private IEnumerator ApplyBuff(CharacterBase character)
    {
        float origPhys = character.Stats.physicalResistance;
        float origMag = character.Stats.magicalResistance;
        character.Stats.physicalResistance += origPhys * defenseBonusPercent;
        character.Stats.magicalResistance += origMag * defenseBonusPercent;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            character.Stats.Heal(character.Stats.maxHP * lifeRegenPercentPerSecond * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }
        character.Stats.physicalResistance = origPhys;
        character.Stats.magicalResistance = origMag;
        TriggerInternalCooldownVisual(character);
    }

    private void TriggerInternalCooldownVisual(CharacterBase character)
    {
        // Optionally notify UI that passive is on internal cooldown.
    }
}
