using UnityEngine;

public enum ItemRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}

[CreateAssetMenu(menuName = "Items/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public ItemRarity rarity;
    public EquipmentSlot slot; 
    
    [Tooltip("Only used if slot is Weapon")]
    public WeaponType weaponType;

    [Header("Stat Modifiers")]
    public float bonusMaxHP;
    public float bonusMaxMana;
    public float bonusPhysicalDamage;
    public float bonusMagicalDamage;
    public float bonusPhysicalResistance;
    public float bonusMagicalResistance;
    public float bonusMovementSpeed;
    public float bonusAttackSpeed;
    public float bonusCritChance;
    public float bonusCritDamage;
    public float bonusAbilitySpeed;

    [Header("Special Effects")]
    [TextArea] public string specialEffectDescription;
}