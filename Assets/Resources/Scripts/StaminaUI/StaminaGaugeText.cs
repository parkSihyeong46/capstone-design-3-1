using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaGaugeText : MonoBehaviour
{
    Text staminaGaugeText;
    private void Awake()
    {
        staminaGaugeText = GetComponent<Text>();
    }

    private void Start()
    {
        SetColor(0);
    }

    public void UpdateStaminaText(int currentStamina, int maxStamina)
    {
        staminaGaugeText.text = currentStamina.ToString() + " / " + maxStamina.ToString();
    }

    public void SetColor(float alpha)
    {
        Color color = staminaGaugeText.color;
        color.a = alpha;
        staminaGaugeText.color = color;
    }
}
