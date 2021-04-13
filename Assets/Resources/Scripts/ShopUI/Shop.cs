using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Shop : MonoBehaviour
{
    private const string traderDataPath = "Data/traderData";
    private TextAsset textAsset;
    private List<CellItem> cellItemList = new List<CellItem>();

    [SerializeField]
    private Transform productParent;
    private Transform productPrefab;

    private List<Product> productList = new List<Product>();

    public class CellItem
    {
        public Item item;
        public int count;
        public int price = -1;
        public CellItem(Item item, int count)
        {
            this.item = item;
            this.count = count;
        }
        public CellItem(Item item, int count, int price)
        {
            this.item = item;
            this.count = count;
            this.price = price;
        }
    }

    private void Awake()
    {
        productPrefab = Resources.Load<Transform>("Prefabs/shop/productButton");

        textAsset = Resources.Load<TextAsset>(traderDataPath);
        InitCellItemList();
        for (int i = 0; i < cellItemList.Count; i++)
        {
            if (cellItemList[i] == null)
                continue;

            CreateProductButton(new CellItem(cellItemList[i].item, cellItemList[i].count));
        }
    }
    private void OnEnable()
    {
        PriceDataManager.instance.onChangePriceData += SyncProductUI;
        PriceDataManager.instance.InitPriceData();

        GameManager.instance.isOpenShop = true;
    }
    private void OnDisable()
    {
        PriceDataManager.instance.onChangePriceData -= SyncProductUI;

        GameManager.instance.isOpenShop = false;
    }
    public void SyncProductUI()
    {
        InitCellItemList();
        for (int i = 0; i < productList.Count; i++)
        {
            productList[i].RepaintPrintData(cellItemList[i]);
        }
    }

    public List<CellItem> GetCellItemList()
    {
        return cellItemList;
    }
    private void InitCellItemList()
    {
        string[] line = textAsset.text.Substring(0, textAsset.text.Length - 1).Split('\n');
        cellItemList.Clear();

        for (int i = 1; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            if(0 > PriceDataManager.instance.GetPrice(row[2]))
            {
                try
                {
                    cellItemList.Add(new CellItem(ItemManager.Instance.GetItem(Int32.Parse(row[0])).DeepCopy(), Int32.Parse(row[3])));
                }
                catch
                {
                    cellItemList.Add(null);
                }
            }
            else
            {
                try
                {
                    cellItemList.Add(new CellItem(ItemManager.Instance.GetItem(Int32.Parse(row[0])).DeepCopy(), Int32.Parse(row[3]), 
                        PriceDataManager.instance.GetPrice(row[2])));
                }
                catch
                {
                    cellItemList.Add(null);
                }
            }
            
        }
    }
    void CreateProductButton(CellItem cellItem)
    {
        Transform product = Instantiate(productPrefab);
        product.SetParent(productParent);

        Product productScripts = product.GetComponent<Product>();
        productScripts.RepaintPrintData(new CellItem(cellItem.item, cellItem.count));
        productList.Add(productScripts);
    }
}
