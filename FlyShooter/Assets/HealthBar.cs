using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public RectTransform mask;
    public health1 health;

    float originalWidth;

    void Start()
    {
        originalWidth = mask.sizeDelta.x;

        UpdateHealthValue();

        health.onHealthChanged += UpdateHealthValue;
    }

    void UpdateHealthValue()
    {
        float scale = (float)health.healthPoint / health.defaultHealthPoint;

        mask.sizeDelta = new Vector2(scale * originalWidth, mask.sizeDelta.y);
    }
}