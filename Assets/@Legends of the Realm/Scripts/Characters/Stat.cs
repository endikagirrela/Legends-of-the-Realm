using UnityEngine;

[System.Serializable]
public class Stat
{
    public float baseValue;

    public float flatBonus = 0f;          // Bonos permanentes (equipo, pasivas)
    public float flatBonusBuff = 0f;      // Bonos temporales (buffs)
    public float flatBonusDebuff = 0f;    // Penalizaciones temporales (debuffs)

    public float multiplier = 1f;         // Multiplicador total (buffs/debuffs por porcentaje)

    public float TotalValue =>
        Mathf.Max((baseValue + flatBonus + flatBonusBuff - flatBonusDebuff) * multiplier, 0f);

    public void ResetModifiers()
    {
        flatBonus = 0f;
        flatBonusBuff = 0f;
        flatBonusDebuff = 0f;
        multiplier = 1f;
    }
}
