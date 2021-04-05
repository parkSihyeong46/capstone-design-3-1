using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMoney : MonoBehaviour
{
    [SerializeField]
    private Text moneyText;
    private void OnEnable()
    {
        Inventory.Instance.onChangeItem += UpdateUI;
    }
    private void Start()
    {
        UpdateUI();
    }

    private void OnDestroy()
    {
        Inventory.Instance.onChangeItem -= UpdateUI;
    }
    private void UpdateUI()
    {
        moneyText.text = Inventory.Instance.Money.ToString();
    }
}
