using UnityEngine;
using TMPro;

public class FloatingDamageText : MonoBehaviour
{
    public TextMeshPro text;
    public float floatSpeed = 2f;
    public float lifetime = 1f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
        transform.LookAt(Camera.main.transform); // Mira siempre al jugador
    }

    public void SetText(string damage, Color color)
    {
        text.text = damage;
        text.color = color;
    }
}
