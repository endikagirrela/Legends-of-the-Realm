using UnityEngine;

public class KnockdownEffect : StatusEffect
{
    private float originalMovementSpeed;
    private float disableDuration;
    private bool isKnockedDown = false;

    public KnockdownEffect(float duration)
    {
        this.duration = duration;
    }

    protected override void OnApply()
    {
        if (target == null) return;

        // Simula caída: bloquea movimiento/ataques (por ejemplo reduciendo velocidad a 0)
        originalMovementSpeed = target.Stats.movementSpeed;
        target.Stats.movementSpeed = 0f;
        // Aquí podrías también desactivar input/IA con flags en tu sistema.
        isKnockedDown = true;

        // Opcional: aplicar una pequeña fuerza hacia abajo o animación de caída
        if (target.TryGetComponent<Rigidbody>(out var rb))
        {
            // Empuja ligeramente hacia abajo para “pegar al suelo”
            rb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
        }

        // Puedes disparar evento visual/sonoro de knockdown aquí.
    }

    public override void Tick()
    {
        base.Tick();
        // podrías agregar lógica intermedia si quieres (por ejemplo, parpadeo justo antes de levantarse)
    }

    public override void OnExpire()
    {
        if (target == null) return;

        // Restaurar movilidad
        target.Stats.movementSpeed = originalMovementSpeed;
        isKnockedDown = false;

        // Opcional: tiempo de “levantarse” adicional o transición
        // Ejemplo simple: podrías retrasar reactivar input unos 0.2s más si tu sistema lo soporta.
    }
}
