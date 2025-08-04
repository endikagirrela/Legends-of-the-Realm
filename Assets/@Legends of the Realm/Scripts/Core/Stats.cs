using UnityEngine;

[System.Serializable]
public class Stats
{
    // Core resources
    public float maxHP = 100f;
    public float currentHP = 100f;
    public float maxMana = 50f;
    public float currentMana = 50f;

    // Damage & resistances
    public float physicalDamage = 10f;
    public float magicalDamage = 5f;
    public float physicalResistance = 10f;  // similar to armor
    public float magicalResistance = 10f;   // similar to magic resist
    public float attackRange = 2f;

    // Speed-related
    public float movementSpeed = 5f;
    public float attackSpeed = 1f;           // multiplier (e.g., affects auto-attack rate)
    public float abilitySpeed = 1f;          // multiplier to reduce cooldowns (higher = faster)

    // Critical
    [Range(0f, 1f)] public float critChance = 0.1f;           // 0.1 = 10%
    public float critDamageMultiplier = 1.5f;                // 1.5x damage on crit

    // Helpers
    public bool IsAlive() => currentHP > 0;

    // Damage application

    public void TakePhysicalDamage(float rawAmount)
    {
        float mitigated = rawAmount * (1 - Mathf.Clamp01(physicalResistance / (physicalResistance + 100f)));
        ApplyDamage(mitigated);
    }

    public void TakeMagicalDamage(float rawAmount)
    {
        float mitigated = rawAmount * (1 - Mathf.Clamp01(magicalResistance / (magicalResistance + 100f)));
        ApplyDamage(mitigated);
    }

    private void ApplyDamage(float amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Max(0f, currentHP);
    }

    public void Heal(float amount)
    {
        currentHP = Mathf.Min(maxHP, currentHP + amount);
    }

    public bool SpendMana(float amount)
    {
        if (currentMana >= amount)
        {
            currentMana -= amount;
            return true;
        }
        return false;
    }

    public void RegenerateMana(float amount)
    {
        currentMana = Mathf.Min(maxMana, currentMana + amount);
    }

    // Damage calculation with crit (example usage)
    public float CalculatePhysicalAttackDamage()
    {
        float baseDamage = physicalDamage;
        if (RollCrit())
        {
            return baseDamage * critDamageMultiplier;
        }
        return baseDamage;
    }

    public float CalculateMagicalAttackDamage()
    {
        float baseDamage = magicalDamage;
        if (RollCrit())
        {
            return baseDamage * critDamageMultiplier;
        }
        return baseDamage;
    }

    private bool RollCrit()
    {
        return Random.value < critChance;
    }

    // Cooldown adjustment helper: effective cooldown = base / abilitySpeed
    public float AdjustedCooldown(float baseCooldown)
    {
        if (abilitySpeed <= 0f) return baseCooldown;
        return baseCooldown / abilitySpeed;
    }
}
