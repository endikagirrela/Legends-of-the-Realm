public class StatusEffectInstance
{
    public StatusEffect effect;
    public float remainingDuration;
    public float magnitude;

    public StatusEffectInstance(StatusEffect effect, float magnitude, float customDuration)
    {
        this.effect = effect;
        this.remainingDuration = customDuration;
        this.magnitude = magnitude;
    }

    public void Tick(float deltaTime)
    {
        remainingDuration -= deltaTime;
    }

    public bool IsExpired => remainingDuration <= 0f;
}
