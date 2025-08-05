using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public AutoAttackController autoAttack;
    public string characterName = "Unnamed";
    public CharacterType type = CharacterType.Player;
    public CharacterStats stats = new CharacterStats();

    public delegate void OnDeath(CharacterBase character);
    public event OnDeath onDeath;

    public bool isStunned = false;
    public bool isSilenced = false;
    public bool isFrozen = false;

    protected virtual void Start()
    {
        autoAttack = GetComponent<AutoAttackController>();
    }

    public virtual void TakePhysicalDamage(float damage)
    {
        float taken = stats.TakePhysicalDamage(damage);
        DamageDisplayManager.Instance?.ShowDamage(transform.position, taken);

        Debug.Log($"{characterName} took {taken} physical damage. HP: {stats.currentHP}/{stats.maxHP}");

        OnDamaged(stats.currentHP);

        if (stats.IsDead) Die();
    }

    public virtual void TakeMagicalDamage(float damage)
    {
        float taken = stats.TakeMagicalDamage(damage);
        DamageDisplayManager.Instance?.ShowDamage(transform.position, taken);
        Debug.Log($"{characterName} took {taken} magical damage. HP: {stats.currentHP}/{stats.maxHP}");

        OnDamaged(stats.currentHP);

        if (stats.IsDead) Die();
    }

    protected virtual void OnDamaged(float newHP)
    {
        
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

    public void EngageTarget(CharacterBase target)
    {
        if (autoAttack != null)
            autoAttack.StartAutoAttack(target);
    }

    public void InterruptAutoAttack()
    {
        if (autoAttack != null)
            autoAttack.PauseAutoAttack();
    }

    public void ResumeAutoAttack(CharacterBase target)
    {
        if (autoAttack != null)
            autoAttack.ResumeAutoAttack(target);
    }

    public void StopAutoAttack()
    {
        if (autoAttack != null)
            autoAttack.StopAutoAttack();
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

    public bool IsInRange(CharacterBase target)
    {
        if (target == null) return false;
        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= stats.attackRange.TotalValue;
    }
}
