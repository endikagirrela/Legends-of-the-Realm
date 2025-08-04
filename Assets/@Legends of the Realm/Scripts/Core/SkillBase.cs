using UnityEngine;
using System.Collections;

public abstract class SkillBase : ScriptableObject
{
    public string skillName;
    [TextArea] public string description;
    public float cooldown = 5f;
    [HideInInspector] public float lastUsedTime = -999f;
    public bool ignoresGlobalLock = false;

    public abstract IEnumerator Execute(CharacterBase user, CharacterBase target);

    public bool IsReady()
    {
        return Time.time >= lastUsedTime + cooldown;
    }

    public void TriggerCooldown()
    {
        lastUsedTime = Time.time;
    }
}
