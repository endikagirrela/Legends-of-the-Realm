using UnityEngine;

public class ComboController : MonoBehaviour
{
    private int currentCombo = 0;
    public float comboResetTime = 1f;
    private float lastHitTime;

    public void RegisterHit()
    {
        if (Time.time - lastHitTime > comboResetTime)
        {
            currentCombo = 0;
        }
        currentCombo++;
        lastHitTime = Time.time;
        // Expose combo state if needed
    }

    public int GetCombo() => currentCombo;
}
