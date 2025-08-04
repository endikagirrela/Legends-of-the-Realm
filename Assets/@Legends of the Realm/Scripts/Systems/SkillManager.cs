using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public CharacterBase character;

    public void TryUse(int idx)
    {
        // Placeholder target selection; you'd replace this with real targeting
        CharacterBase target = FindClosestEnemy();
        if (target != null)
        {
            character.UseSkill(idx, target);
        }
    }

    private CharacterBase FindClosestEnemy()
    {
        // Simplified: find any enemy in scene
        CharacterBase[] all = FindObjectsOfType<CharacterBase>();
        foreach (var c in all)
        {
            if (c != character && c.IsEnemyTo(character)) return c;
        }
        return null;
    }
}
