using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Item item = null;

    private Image toolTipBackground;
    private Image itemImage;
    private Text itemTitleText;
    private Text itemExplainText;

    private void Awake()
    {
        toolTipBackground = transform.GetComponent<Image>();
        itemImage = transform.GetChild(0).GetComponent<Image>();
        itemTitleText = transform.GetChild(1).GetComponent<Text>();
        itemExplainText = transform.GetChild(2).GetComponent<Text>();
    }

    public void SetColor(float alpha)
    {
        Color color = toolTipBackground.color;
        color.a = alpha;
        toolTipBackground.color = color;

        color = itemImage.color;
        color.a = alpha;
        itemImage.color = color;

        color = itemTitleText.color;
        color.a = alpha;
        itemTitleText.color = color;

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

        itemImage.sprite = item.ItemImage;
        itemTitleText.text = item.ItemName;
        itemExplainText.text = item.ItemExplain;
    }
}
