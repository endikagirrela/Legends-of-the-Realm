using UnityEngine;

public class Dummy : CharacterBase
{
    protected override void Start()
    {
        base.Start();
        
    }

    protected override void Die()
    {
        base.Die();
        Debug.Log($"{characterName} ha sido derrotado.");
    }
}
