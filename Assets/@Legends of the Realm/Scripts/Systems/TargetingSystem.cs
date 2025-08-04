using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public CharacterBase owner;
    public float range = 5f;

    public CharacterBase GetNearestEnemy()
    {
        CharacterBase[] all = FindObjectsOfType<CharacterBase>();
        CharacterBase best = null;
        float bestDist = float.MaxValue;
        foreach (var c in all)
        {
            if (c == owner || !c.IsEnemyTo(owner)) continue;
            float d = Vector3.Distance(owner.transform.position, c.transform.position);
            if (d < bestDist && d <= range)
            {
                bestDist = d;
                best = c;
            }
        }
        return best;
    }
}
