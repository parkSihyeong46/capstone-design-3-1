using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Item item = null;

    [SerializeField]
    private Image toolTipTitleBackground;
    [SerializeField]
    private Image toolTipExplainBackground;
    [SerializeField]
    private Text itemTitleText;
    [SerializeField]
    private Text itemTypeText;
    [SerializeField]
    private Text itemExplainText;

    public void SetColor(float alpha)
    {
        Color color = toolTipTitleBackground.color;
        color.a = alpha;
        toolTipTitleBackground.color = color;

        color = toolTipExplainBackground.color;
        color.a = alpha;
        toolTipExplainBackground.color = color;

        color = itemTitleText.color;
        color.a = alpha;
        itemTitleText.color = color;

        color = itemTypeText.color;
        color.a = alpha;
        itemTypeText.color = color;

        color = itemExplainText.color;
        color.a = alpha;
        itemExplainText.color = color;
    }

        public void SetToolTipItem(Item item)
    {
        this.item = item;

        RepaintToolTip();
    }

    private void RepaintToolTip()
    {
        if (item == null)
            return;

        itemTitleText.text = item.ItemName;
        itemExplainText.text = item.ItemExplain;

        string itemTypeString;
        switch (item.ItemType)
        {
            case Item.ItemTypes.Tool:
                itemTypeString = "도구";
                break;
            case Item.ItemTypes.Seed:
                itemTypeString = "씨앗";
                break;
            case Item.ItemTypes.Food:
                itemTypeString = "음식";
                break;
            case Item.ItemTypes.Equipment:
                itemTypeString = "장비";
                break;
            case Item.ItemTypes.Material:
                itemTypeString = "재료";
                break;
            default:
                itemTypeString = "";
                break;
        }

        itemTypeText.text = itemTypeString;
    }
}
