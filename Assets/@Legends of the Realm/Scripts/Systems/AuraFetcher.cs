using System.Collections.Generic;
using UnityEngine;

public static class AuraFetcher
{
    // Example: get allies behind user within width
    public static List<CharacterBase> GetAlliesBehind(CharacterBase user, float width)
    {
        List<CharacterBase> result = new List<CharacterBase>();
        CharacterBase[] all = GameObject.FindObjectsOfType<CharacterBase>();
        foreach (var c in all)
        {
            if (c == user) continue;
            if (!IsAlly(user, c)) continue;
            Vector3 to = c.transform.position - user.transform.position;
            float angle = Vector3.Angle(user.transform.forward, to);
            if (angle > 90f)
            { // behind roughly
                if (to.magnitude <= width + 1f) result.Add(c);
            }
        }
        return result;
    }

    private static bool IsAlly(CharacterBase a, CharacterBase b)
    {
        // Replace with real team logic
        return !a.IsEnemyTo(b);
    }
}
