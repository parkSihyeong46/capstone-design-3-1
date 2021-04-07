using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    private int maxStamina = 300;
    private int stamina = 200;
    private const int staminaBarHeight = 40;

    RectTransform rectTransform;
    private float gaugePercent = 1.0f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        TransformStaminaGauge();
    }
    public void AddStamina(int stamina)
    {
        if (this.stamina + stamina > maxStamina)
        {
            this.stamina = maxStamina;
        }
        else
        {
            this.stamina += stamina;
        }

        TransformStaminaGauge();
    }

    public void UseStamina(int stamina)
    {
        this.stamina -= stamina;

        if (this.stamina < 0)
            Debug.Log("스태미나 0보다 작음, 탈진");

        TransformStaminaGauge();
    }

    public void TransformStaminaGauge()
    {
        gaugePercent = (float)stamina / maxStamina;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, staminaBarHeight * gaugePercent);
        Debug.Log(stamina);
    }
}
