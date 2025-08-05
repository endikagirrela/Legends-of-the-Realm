using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private CharacterBase character;

    private Dictionary<EquipmentSlot, EquipmentItem> equippedItems = new();

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    public void EquipItem(EquipmentItem item)
    {
        if (item == null) return;

        // Des-equipar si ya había algo en esa ranura
        if (equippedItems.ContainsKey(item.slot))
            UnequipItem(item.slot);

        equippedItems[item.slot] = item;
        ApplyItemStats(item, apply: true);
    }

    public void UnequipItem(EquipmentSlot slot)
    {
        if (equippedItems.TryGetValue(slot, out EquipmentItem item))
        {
            ApplyItemStats(item, apply: false);
            equippedItems.Remove(slot);
        }
    }

    private void ApplyItemStats(EquipmentItem item, bool apply)
    {
        float m = apply ? 1f : -1f;
        var stats = character.stats;

        stats.maxHP.flatBonus += item.bonusHP * m;
        stats.maxMana.flatBonus += item.bonusMana * m;
        stats.physicalDamage.flatBonus += item.bonusPhysicalDamage * m;
        stats.magicalDamage.flatBonus += item.bonusMagicalDamage * m;
        stats.physicalResistance.flatBonus += item.bonusPhysicalResistance * m;
        stats.magicalResistance.flatBonus += item.bonusMagicalResistance * m;
        stats.attackSpeed.flatBonus += item.bonusAttackSpeed * m;
        stats.movementSpeed.flatBonus += item.bonusMovementSpeed * m;
        stats.attackRange.flatBonus += item.bonusAttackRange * m;
        stats.critChance.flatBonus += item.bonusCritChance * m;
        stats.critDamageMultiplier.flatBonus += item.bonusCritMultiplier * m;
    }

}
