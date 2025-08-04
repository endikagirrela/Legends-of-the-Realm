using System;
using UnityEngine;

public class AdvancedKnockdownEffect : StatusEffect
{
    // Configurables
    private float knockdownDuration;          // tiempo en el suelo
    private float recoveryDelay = 0.2f;       // tiempo extra antes de volver a moverse
    private float restoreTime = 0.5f;         // tiempo en el que se regresa gradualmente a la velocidad original

    // Estado interno
    private float originalMovementSpeed;
    private bool isInRecovery = false;

    // Callbacks (puedes suscribir animadores / VFX)
    public Action OnKnockdownStart;
    public Action OnStartGettingUp;
    public Action OnFullyRecovered;

    public AdvancedKnockdownEffect(float knockdownDuration, float recoveryDelay = 0.2f, float restoreTime = 0.5f)
    {
        this.knockdownDuration = knockdownDuration;
        this.recoveryDelay = recoveryDelay;
        this.restoreTime = restoreTime;
        this.duration = knockdownDuration + recoveryDelay + restoreTime; // para el sistema general, aunque manejamos fases
    }

    protected override void OnApply()
    {
        if (target == null) return;

        originalMovementSpeed = target.Stats.movementSpeed;
        // Caída: inmoviliza completamente
        target.Stats.movementSpeed = 0f;

        OnKnockdownStart?.Invoke();
        // Inicia la secuencia de recuperación
        target.StartCoroutine(KnockdownSequence());
    }

    private System.Collections.IEnumerator KnockdownSequence()
    {
        // 1. Knockdown en el suelo
        float elapsed = 0f;
        while (elapsed < knockdownDuration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 2. Inicio de levantarse (delay)
        OnStartGettingUp?.Invoke();
        isInRecovery = true;
        yield return new WaitForSeconds(recoveryDelay);

        // 3. Restauración gradual de velocidad
        float restoreElapsed = 0f;
        while (restoreElapsed < restoreTime)
        {
            float t = restoreElapsed / restoreTime;
            target.Stats.movementSpeed = Mathf.Lerp(0f, originalMovementSpeed, t);
            restoreElapsed += Time.deltaTime;
            yield return null;
        }

        // 4. Completado
        target.Stats.movementSpeed = originalMovementSpeed;
        OnFullyRecovered?.Invoke();

        // Marca como expirado para que el sistema lo limpie
        elapsed = knockdownDuration + recoveryDelay + restoreTime;
        this.elapsed = elapsed;
    }

    public override void Tick()
    {
        // No hace nada aquí porque la lógica está en la coroutine.
    }

    public override void OnExpire()
    {
        if (target == null) return;
        // Asegurar que velocidad se restaura si algo falló
        target.Stats.movementSpeed = originalMovementSpeed;
    }
}
