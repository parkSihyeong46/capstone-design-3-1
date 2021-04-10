using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickSlotUI : MonoBehaviour
{
    private const int QUICKSLOT_SIZE = 12;

    Image[] quickSlotItemImage = new Image[QUICKSLOT_SIZE];
    Text[] quickSlotItemText = new Text[QUICKSLOT_SIZE];
    private void Awake()
    {
        for(int i = 0; i < QUICKSLOT_SIZE; i++)
        {
            Transform child = transform.GetChild(i);

            quickSlotItemImage[i] = child.GetChild(0).GetComponent<Image>();
            quickSlotItemText[i] = child.GetChild(1).GetComponent<Text>();
        }
    }
    void Start()
    {
        Inventory.Instance.onChangeItem += UpdateUI;
        UpdateUI();
    }
    private void OnDisable()
    {
        Inventory.Instance.onChangeItem -= UpdateUI;
    }
    private void UpdateUI()
    {
        int i = 0;
        foreach (Item inventoryItems in Inventory.Instance.GetItems())
        {
            if (i >= QUICKSLOT_SIZE)
                break;

            if (inventoryItems == null)
            {
                SetColor(0, i, true);
                i++;
                continue;
            }

            if(inventoryItems.IsPrintCount)
            {
                SetColor(1, i, true);
                quickSlotItemText[i].text = inventoryItems.Count.ToString();
            }
            else
            {
                SetColor(1, i, false);
            }

            quickSlotItemImage[i].sprite = inventoryItems.ItemImage;
            i++;
        }
    }

    private void SetColor(float alpha, int index, bool isPrintCount)
    {
        Color color = quickSlotItemImage[index].color;
        color.a = alpha;
        quickSlotItemImage[index].color = color;


        if(isPrintCount)
        {
            color = quickSlotItemText[index].color;
            color.a = alpha;
            quickSlotItemText[index].color = color;
        }
    }
}
