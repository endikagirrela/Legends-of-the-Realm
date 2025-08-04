using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/RoyalGuard/TwinSlash")]
public class SkillTwinSlash : SkillBase
{
    public float totalDamagePercent = 1.6f;
    public float dashDistance = 1f;
    public float dashSpeed = 5f;

    public override IEnumerator Execute(CharacterBase user, CharacterBase target)
    {
        if (!IsReady()) yield break;
        yield return user.StartCoroutine(DashAndHit(user, target));
    }

    private IEnumerator DashAndHit(CharacterBase user, CharacterBase target)
    {
        Vector3 start = user.transform.position;
        Vector3 end = start + user.transform.forward * dashDistance;
        float elapsed = 0f;
        float duration = dashDistance / dashSpeed;

        while (elapsed < duration)
        {
            user.transform.position = Vector3.Lerp(start, end, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        user.transform.position = end;

        float damage = user.Stats.physicalDamage * totalDamagePercent;
        target.TakePhysicalDamage(damage);
        TriggerCooldown();
    }
}
