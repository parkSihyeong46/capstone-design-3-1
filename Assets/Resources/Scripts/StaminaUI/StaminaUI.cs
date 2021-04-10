using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaUI : MonoBehaviour
{
    StaminaBar staminaBar;
    StaminaGaugeText staminaGuageText;
    private void Awake()
    {
        staminaBar = transform.GetChild(0).GetComponent<StaminaBar>();
        staminaGuageText = transform.GetChild(1).GetComponent<StaminaGaugeText>();
    }

    public void OnMouseEnter()
    {
        staminaGuageText.UpdateStaminaText(staminaBar.stamina, staminaBar.maxStamina);
        staminaGuageText.SetColor(1);

        Debug.Log("enter test");
    }

    public void OnMouseExit()
    {
        staminaGuageText.SetColor(0);
    }
}
