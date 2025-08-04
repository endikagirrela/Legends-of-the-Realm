using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public abstract class CharacterBase : MonoBehaviour
{
    public Stats Stats = new Stats();
    public List<SkillBase> ActiveSkills = new List<SkillBase>();
    public PassiveSkillBase PassiveSkill;

    private List<StatusEffect> activeEffects = new List<StatusEffect>();

    private bool isUsingSkill = false;
    public virtual void Awake() { }

    public virtual void Start()
    {
        PassiveSkill?.ApplyPassiveEffect(this);
    }

    public void UseSkill(int index, CharacterBase target)
    {
        if (index < 0 || index >= ActiveSkills.Count) return;
        SkillBase skill = ActiveSkills[index];
        if (!skill.IsReady()) return;
        if (isUsingSkill && !skill.ignoresGlobalLock) return;

        StartCoroutine(UseSkillRoutine(skill, target));
    }
    private IEnumerator UseSkillRoutine(SkillBase skill, CharacterBase target)
    {
        isUsingSkill = true;
        yield return skill.Execute(this, target); // espera a que termine la skill
        skill.TriggerCooldown();
        isUsingSkill = false;
    }

    public void TakeMagicalDamage(float amount)
    {
        Stats.TakeMagicalDamage(amount);
        if (!Stats.IsAlive()) Die();
    }
    public void TakePhysicalDamage(float amount)
    {
        Stats.TakePhysicalDamage(amount);
        if (!Stats.IsAlive()) Die();
    }

    public void Heal(float amount)
    {
        Stats.Heal(amount);
    }

    public void ApplyStatusEffect(StatusEffect effect)
    {
        effect.Initialize(this);
        activeEffects.Add(effect);
    }

    public void RemoveStatusEffect(StatusEffect effect)
    {
        activeEffects.Remove(effect);
    }

    private void Update()
    {
        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            activeEffects[i].Tick();
            if (activeEffects[i].IsExpired)
            {
                activeEffects[i].OnExpire();
                activeEffects.RemoveAt(i);
            }
        }
    }
    public T GetEffect<T>() where T : StatusEffect
    {
        for (int i = 0; i < activeEffects.Count; i++)
        {
            if (activeEffects[i] is T effect)
                return effect;
        }
        return null;
    }
    public abstract void Die();

    // Placeholder: define alliance logic
    public virtual bool IsEnemyTo(CharacterBase other)
    {
        return true; // Replace with team check
    }

    public void ApplyKnockback(Vector3 force)
    {
        // Simple impulse; requires Rigidbody
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(force, ForceMode.Impulse);
        }
    }
}
