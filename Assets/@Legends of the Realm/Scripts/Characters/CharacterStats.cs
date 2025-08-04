using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    [Header("Health & Mana")]
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float maxMana = 50f;
    public float currentMana = 50f;

    [Header("Offensive Stats")]
    public float physicalDamage = 10f;
    public float magicalDamage = 5f;

    [Header("Defensive Stats")]
    public float physicalResistance = 10f;  // Armor
    public float magicalResistance = 10f;   // Magic resist

    [Header("Combat Mechanics")]
    public float attackRange = 2f;
    public float movementSpeed = 5f;
    public float attackSpeed = 1f;          // Multiplier
    public float abilitySpeed = 1f;         // Affects cast time, animation speed, etc.

    [Header("Critical")]
    [Range(0f, 1f)] public float critChance = 0.1f;
    public float critDamageMultiplier = 1.5f;

    // Método para recibir daño físico
    public float TakePhysicalDamage(float damage)
    {
        float finalDamage = Mathf.Max(damage - physicalResistance, 1f);
        currentHP = Mathf.Max(currentHP - finalDamage, 0f);
        return finalDamage;
    }

    // Método para recibir daño mágico
    public float TakeMagicalDamage(float damage)
    {
        float finalDamage = Mathf.Max(damage - magicalResistance, 1f);
        currentHP = Mathf.Max(currentHP - finalDamage, 0f);
        return finalDamage;
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }

    public void RestoreMana(float amount)
    {
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

    public bool IsDead => currentHP <= 0f;
}
