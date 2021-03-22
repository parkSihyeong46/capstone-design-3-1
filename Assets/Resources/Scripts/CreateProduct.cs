using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProduct : MonoBehaviour
{
    [SerializeField]
    private Transform productPrefab;

    void Start()
    {
        List<Shop.CellItem> cellItems = transform.parent.parent.GetComponent<Shop>().GetCellItemList();

        for(int i = 0; i < cellItems.Count; i++)
        {
            if (cellItems[i] == null)
                continue;

            CreateProductButton(cellItems[i]);
        }
    }

    void CreateProductButton(Shop.CellItem cellItem)
    {
        Transform product = Instantiate(productPrefab);
        product.SetParent(transform);

        product.GetChild(0).GetComponent<Text>().text = cellItem.item.ItemName;
        product.GetChild(2).GetComponent<Image>().sprite = cellItem.item.ItemImage;
    }
}
