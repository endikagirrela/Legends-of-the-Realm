using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    public CharacterBase currentTarget;

    [Header("Opcional")]
    public GameObject targetIndicator; // Por ejemplo, un anillo debajo del objetivo

    public void SetTarget(CharacterBase target)
    {
        currentTarget = target;

        if (targetIndicator != null)
        {
            targetIndicator.transform.SetParent(target.transform);
            targetIndicator.transform.localPosition = Vector3.zero;
            targetIndicator.SetActive(true);
        }
    }

    public void ClearTarget()
    {
        currentTarget = null;

        if (targetIndicator != null)
        {
            targetIndicator.transform.SetParent(null);
            targetIndicator.SetActive(false);
        }
    }

    public bool HasTarget => currentTarget != null && !currentTarget.stats.IsDead;
}
