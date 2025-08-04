using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Dictionary<EquipmentSlot, ItemData> equippedItems = new Dictionary<EquipmentSlot, ItemData>();
    private CharacterBase character;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    // Reglas de qué puede equipar cada clase; puedes expandir aquí o delegar a la clase concreta
    private bool CanEquip(ItemData item)
    {
        if (item == null) return false;

        switch (character)
        {
            case RoyalGuard _:
                if (item.slot == EquipmentSlot.Weapon)
                {
                    return item.weaponType == WeaponType.ShortSword || item.weaponType == WeaponType.Shield;
                }
                if (item.slot == EquipmentSlot.OffHand)
                {
                    return item.weaponType == WeaponType.Shield;
                }
                break;

            case Steelbreaker _:
                if (item.slot == EquipmentSlot.Weapon)
                {
                    return item.weaponType == WeaponType.GreatSword;
                }
                if (item.slot == EquipmentSlot.OffHand)
                {
                    return false;
                }
                break;


            default:
                return true;
        }

        return false;
    }

    public void Equip(ItemData item)
    {
        if (item == null) return;

        if (!CanEquip(item))
        {
            Debug.LogWarning($"Cannot equip {item.itemName} on {character.name}: restriction violated.");
            return;
        }

        // Reemplazar si ya hay algo en ese slot
        if (equippedItems.ContainsKey(item.slot))
        {
            Unequip(item.slot);
        }

        equippedItems[item.slot] = item;
        ApplyItemStats(item);
    }

    public void Unequip(EquipmentSlot slot)
    {
        if (equippedItems.ContainsKey(slot))
        {
            RemoveItemStats(equippedItems[slot]);
            equippedItems.Remove(slot);
        }
    }

    private void ApplyItemStats(ItemData item)
    {
        character.Stats.maxHP += item.bonusMaxHP;
        character.Stats.maxMana += item.bonusMaxMana;
        character.Stats.physicalDamage += item.bonusPhysicalDamage;
        character.Stats.magicalDamage += item.bonusMagicalDamage;
        character.Stats.physicalResistance += item.bonusPhysicalResistance;
        character.Stats.magicalResistance += item.bonusMagicalResistance;
        character.Stats.movementSpeed += item.bonusMovementSpeed;
        character.Stats.attackSpeed += item.bonusAttackSpeed;
        character.Stats.critChance += item.bonusCritChance;
        character.Stats.critDamageMultiplier += item.bonusCritDamage;
        character.Stats.abilitySpeed += item.bonusAbilitySpeed;
    }

    private void RemoveItemStats(ItemData item)
    {
        character.Stats.maxHP -= item.bonusMaxHP;
        character.Stats.maxMana -= item.bonusMaxMana;
        character.Stats.physicalDamage -= item.bonusPhysicalDamage;
        character.Stats.magicalDamage -= item.bonusMagicalDamage;
        character.Stats.physicalResistance -= item.bonusPhysicalResistance;
        character.Stats.magicalResistance -= item.bonusMagicalResistance;
        character.Stats.movementSpeed -= item.bonusMovementSpeed;
        character.Stats.attackSpeed -= item.bonusAttackSpeed;
        character.Stats.critChance -= item.bonusCritChance;
        character.Stats.critDamageMultiplier -= item.bonusCritDamage;
        character.Stats.abilitySpeed -= item.bonusAbilitySpeed;
    }
}
