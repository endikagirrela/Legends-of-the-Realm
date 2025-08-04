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

        // Simula ca�da: bloquea movimiento/ataques (por ejemplo reduciendo velocidad a 0)
        originalMovementSpeed = target.Stats.movementSpeed;
        target.Stats.movementSpeed = 0f;
        // Aqu� podr�as tambi�n desactivar input/IA con flags en tu sistema.
        isKnockedDown = true;

        // Opcional: aplicar una peque�a fuerza hacia abajo o animaci�n de ca�da
        if (target.TryGetComponent<Rigidbody>(out var rb))
        {
            // Empuja ligeramente hacia abajo para �pegar al suelo�
            rb.AddForce(Vector3.down * 2f, ForceMode.Impulse);
        }

        // Puedes disparar evento visual/sonoro de knockdown aqu�.
    }

    public override void Tick()
    {
        base.Tick();
        // podr�as agregar l�gica intermedia si quieres (por ejemplo, parpadeo justo antes de levantarse)
    }

    public override void OnExpire()
    {
        if (target == null) return;

        // Restaurar movilidad
        target.Stats.movementSpeed = originalMovementSpeed;
        isKnockedDown = false;

        // Opcional: tiempo de �levantarse� adicional o transici�n
        // Ejemplo simple: podr�as retrasar reactivar input unos 0.2s m�s si tu sistema lo soporta.
    }
}
