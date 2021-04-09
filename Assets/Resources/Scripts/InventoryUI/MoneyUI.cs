using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    private Text moneyText;

    private void Awake()
    {
        moneyText = GetComponent<Text>();
    }
    private void UpdateMoneyText()  // 돈 UI 업데이트
    {
        moneyText.text = Inventory.Instance.Money.ToString();
    }
    void Start()
    {
        UpdateMoneyText();
    }
    private void OnEnable()
    {
        Inventory.Instance.onChangeItem += UpdateMoneyText;
    }
    private void OnDisable()
    {
        Inventory.Instance.onChangeItem -= UpdateMoneyText;
    }
}
