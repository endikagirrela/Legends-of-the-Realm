using UnityEngine;

public class DamageDisplayManager : MonoBehaviour
{
    public static DamageDisplayManager Instance;

    public GameObject popupPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDamage(Vector3 position, float amount, bool isCrit = false)
    {
        GameObject popup = Instantiate(popupPrefab, position + Vector3.up * 1.5f, Quaternion.identity);
        var text = popup.GetComponent<FloatingDamageText>();
        Color color = isCrit ? Color.yellow : Color.white;

        text.SetText(Mathf.RoundToInt(amount).ToString(), color);
    }
}
