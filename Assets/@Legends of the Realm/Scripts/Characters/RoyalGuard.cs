using System.Collections.Generic;
using UnityEngine;

public class RoyalGuard : CharacterBase
{
    [Header("Active Skills")]
    public SkillGuardStrike guardStrike;
    public SkillTwinSlash twinSlash;
    public SkillSteelBash steelBash;
    public SkillUnbreakableGuard unbreakableGuard;
    public SkillUnstoppableWall unstoppableWall;
    public SkillShieldSurge shieldSurge;

    private void Awake()
    {
        // Base stat setup
        Stats.maxHP = 200f;
        Stats.currentHP = Stats.maxHP;

        ActiveSkills = new List<SkillBase>() {
            guardStrike,
            twinSlash,
            steelBash,
            unbreakableGuard,
            unstoppableWall,
            shieldSurge
        };
    }

    public override void Die()
    {
        Debug.Log("Royal Guard has fallen.");
        // Add death logic/animation here
    }
}
