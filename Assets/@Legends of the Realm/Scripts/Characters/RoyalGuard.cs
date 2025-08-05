using System.Collections.Generic;
using UnityEngine;

public class RoyalGuard : CharacterBase
{
    public PassiveLastBastion lastBastionPassive;

    protected override void OnDamaged(float newHP)
    {
        if (lastBastionPassive != null)
            lastBastionPassive.TryTrigger(this);
    }
}
