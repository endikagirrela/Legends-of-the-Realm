using UnityEngine;

[CreateAssetMenu(menuName = "Items/Equipment Item")]
public class EquipmentItem : Item
{
    public EquipmentSlot slot;

    [Header("Stat Bonuses")]
    public float bonusHP;
    public float bonusMana;
    public float bonusPhysicalDamage;
    public float bonusMagicalDamage;
    public float bonusPhysicalResistance;
    public float bonusMagicalResistance;
    public float bonusAttackSpeed;
    public float bonusMovementSpeed;
    public float bonusAttackRange;
    public float bonusCritChance;
    public float bonusCritMultiplier;
}
