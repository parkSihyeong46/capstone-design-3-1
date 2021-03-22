using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMoney : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;
    private void Start()
    {
        Inventory.Instance.onChangeItem += UpdateUI;
        UpdateUI();
    }
    private void UpdateUI()
    {
        moneyText.text = Inventory.Instance.Money.ToString();
    }
}
