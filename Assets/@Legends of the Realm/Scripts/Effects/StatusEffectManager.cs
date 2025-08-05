using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private CharacterBase character;
    private List<StatusEffectInstance> activeEffects = new();

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;

        for (int i = activeEffects.Count - 1; i >= 0; i--)
        {
            var instance = activeEffects[i];
            instance.Tick(deltaTime);

            if (instance.IsExpired)
            {
                instance.effect.RemoveEffect(character, instance);
                activeEffects.RemoveAt(i);
            }
        }
    }

    public void ApplyEffect(StatusEffect effect, float magnitude, float duration)
    {
        // ¿Ya existe un efecto del mismo tipo?
        StatusEffectInstance existing = activeEffects.Find(e => e.effect == effect);

        if (existing != null)
        {
            // Reemplazar solo si el nuevo efecto es mejor
            bool isStronger = magnitude > existing.magnitude;
            bool sameStrengthButLonger = magnitude == existing.magnitude && duration > existing.remainingDuration;

            if (isStronger || sameStrengthButLonger)
            {
                existing.effect.RemoveEffect(character, existing);
                activeEffects.Remove(existing);
            }
            else
            {
                Debug.Log($"{effect.name} no fue aplicado: ya existe uno mejor o igual.");
                return;
            }
        }

        // Aplicar nuevo efecto
        var instance = new StatusEffectInstance(effect, magnitude, duration);
        activeEffects.Add(instance);
        effect.ApplyEffect(character, instance);
    }


}
