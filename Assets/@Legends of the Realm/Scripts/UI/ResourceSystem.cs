using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float regenPerSecond = 5f;

    private void Update()
    {
        currentStamina = Mathf.Min(maxStamina, currentStamina + regenPerSecond * Time.deltaTime);
    }

    public bool Spend(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            return true;
        }
        return false;
    }
}
