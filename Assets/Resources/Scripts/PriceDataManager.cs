using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Networking;
public class PriceDataManager : MonoBehaviour
{
    public delegate void OnChangePriceData();
    public OnChangePriceData onChangePriceData;

    public class PriceData
    {
        public int parsnip;
        public int parsnipSeed;
        public int cauliflower;
        public int cauliflowerSeed;
        public int strawberry;
        public int strawberrySeed;
        public int seeds;
        public int icecream;
        public int survivalHamburger;
        public int friedEgg;
        public int cheeseCauliflower;
    }

    UnityWebRequest www;

    public static PriceDataManager instance = null;
    public PriceData priceData = new PriceData();

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void InitPriceData()
    {
        StartCoroutine(GetPriceData());
    }

    IEnumerator GetPriceData()
    {
        string url = "http://localhost:3000/price";
        www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.error == null)
        {
            priceData = JsonUtility.FromJson<PriceData>(www.downloadHandler.text);
            Debug.Log(www.downloadHandler.text);
        }
        else
        {
            Debug.Log("error");
        }

        onChangePriceData.Invoke();
    }

    public int GetPrice(string itemName)
    {
        FieldInfo typeField1;
        try
        {
            typeField1 = priceData.GetType().GetField(itemName);
            return (int)typeField1.GetValue(priceData);
        }
        catch
        {
            return -1;
        }
    }
}

