using UnityEngine;

public abstract class StatusEffect
{
    protected CharacterBase target;
    protected float duration;
    protected float elapsed;

    public bool IsExpired => elapsed >= duration;

    public virtual void Initialize(CharacterBase character)
    {
        target = character;
        elapsed = 0f;
        OnApply();
    }

    public virtual void Tick()
    {
        elapsed += Time.deltaTime;
        OnTick();
    }

    protected virtual void OnApply() { }
    protected virtual void OnTick() { }
    public virtual void OnExpire() { }
}
