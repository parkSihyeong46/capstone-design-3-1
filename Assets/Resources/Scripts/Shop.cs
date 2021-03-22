using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Shop : MonoBehaviour
{
    private const string traderDataPath = "Data/traderData";
    private TextAsset textAsset;
    private List<CellItem> cellItemList = new List<CellItem>();
    public class CellItem
    {
        public Item item;
        public int count;

        public CellItem(Item item, int count)
        {
            this.item = item;
            this.count = count;
        }
    }

    void Start()
    {
        textAsset = Resources.Load<TextAsset>(traderDataPath);
        string[] line = textAsset.text.Substring(0, textAsset.text.Length - 1).Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            try
            {
                cellItemList.Add(new CellItem(ItemManager.Instance.GetItem(Int32.Parse(row[0])).DeepCopy(), Int32.Parse(row[2])));
            }
            catch
            {
                cellItemList.Add(null);
            }
        }
    }

    public List<CellItem> GetCellItemList()
    {
        return cellItemList;
    }
}
