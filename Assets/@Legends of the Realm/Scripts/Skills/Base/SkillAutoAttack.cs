using UnityEngine;
using System.Collections;

public abstract class SkillAutoAttack : SkillBase
{
    public abstract IEnumerator AutoAttackLoop(CharacterBase user, CharacterBase target);
}
