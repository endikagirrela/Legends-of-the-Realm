using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public string characterName = "Unnamed";
    public CharacterType type = CharacterType.Player;
    public CharacterStats stats = new CharacterStats();

    public delegate void OnDeath(CharacterBase character);
    public event OnDeath onDeath;

    // Recibir daño físico
    public virtual void TakePhysicalDamage(float damage)
    {
        float taken = stats.TakePhysicalDamage(damage);
        Debug.Log($"{characterName} took {taken} physical damage. HP: {stats.currentHP}/{stats.maxHP}");
        if (stats.IsDead) Die();
    }

    // Recibir daño mágico
    public virtual void TakeMagicalDamage(float damage)
    {
        float taken = stats.TakeMagicalDamage(damage);
        Debug.Log($"{characterName} took {taken} magical damage. HP: {stats.currentHP}/{stats.maxHP}");
        if (stats.IsDead) Die();
    }

    public virtual void Heal(float amount)
    {
        stats.Heal(amount);
        Debug.Log($"{characterName} healed {amount}. HP: {stats.currentHP}/{stats.maxHP}");
    }

    public virtual void RestoreMana(float amount)
    {
        stats.RestoreMana(amount);
        Debug.Log($"{characterName} restored {amount} mana. Mana: {stats.currentMana}/{stats.maxMana}");
    }

    protected virtual void Die()
    {
        Debug.Log($"{characterName} has died.");
        onDeath?.Invoke(this);
        gameObject.SetActive(false);
    }

    public bool IsEnemyTo(CharacterBase other)
    {
        return this.type != other.type;
    }
}
