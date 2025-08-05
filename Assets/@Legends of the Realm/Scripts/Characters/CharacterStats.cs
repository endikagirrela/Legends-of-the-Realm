using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [Header("Health & Mana")]
    public Stat maxHP = new Stat { baseValue = 100f };
    public float currentHP = 100f;

    public Stat maxMana = new Stat { baseValue = 50f };
    public float currentMana = 50f;

    [Header("Offensive")]
    public Stat physicalDamage = new Stat { baseValue = 10f };
    public Stat magicalDamage = new Stat { baseValue = 5f };

    [Header("Defensive")]
    public Stat physicalResistance = new Stat { baseValue = 10f };
    public Stat magicalResistance = new Stat { baseValue = 10f };

    [Header("Combat")]
    public Stat attackRange = new Stat { baseValue = 2f };
    public Stat movementSpeed = new Stat { baseValue = 5f };
    public Stat attackSpeed = new Stat { baseValue = 1f };
    public Stat abilitySpeed = new Stat { baseValue = 1f };

    [Header("Critical")]
    public Stat critChance = new Stat { baseValue = 0.1f };
    public Stat critDamageMultiplier = new Stat { baseValue = 1.5f };

    public float TakePhysicalDamage(float damage)
    {
        float final = Mathf.Max(damage - physicalResistance.TotalValue, 1f);
        currentHP = Mathf.Max(currentHP - final, 0f);
        return final;
    }

    public float TakeMagicalDamage(float damage)
    {
        float final = Mathf.Max(damage - magicalResistance.TotalValue, 1f);
        currentHP = Mathf.Max(currentHP - final, 0f);
        return final;
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP.TotalValue);
    }

    public void RestoreMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana.TotalValue);
    }

    public bool IsDead => currentHP <= 0f;
}
