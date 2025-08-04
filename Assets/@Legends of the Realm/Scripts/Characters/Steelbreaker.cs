using System.Collections.Generic;
using UnityEngine;

public class Steelbreaker : CharacterBase
{
    [Header("Active Skills")]
    public SkillHeavySlash heavySlash;
    public SkillSeismicSlam seismicSlam;
    public SkillArmorSplitter armorSplitter;
    public SkillBerserkerFury berserkerFury;
    public SkillColossusStrike colossusStrike;
    public SkillUnstoppableCharge unstoppableCharge;

    private void Awake()
    {
        // Base stats (ajusta según balance)
        Stats.physicalDamage = 15f;
        Stats.attackSpeed = 1f;
        Stats.maxHP = 250f;
        Stats.currentHP = Stats.maxHP;

        ActiveSkills = new List<SkillBase>()
        {
            heavySlash,
            seismicSlam,
            armorSplitter,
            berserkerFury,
            colossusStrike,
            unstoppableCharge
        };
    }

    public override void Die()
    {
        Debug.Log("Steelbreaker has fallen.");
    }
}
