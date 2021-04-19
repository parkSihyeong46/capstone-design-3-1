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
        public int parsnip = -1;
        public int parsnipSeed = -1;
        public int cauliflower = -1;
        public int cauliflowerSeed = -1;
        public int strawberry = -1;
        public int strawberrySeed = -1;
        public int seeds = -1;
        public int icecream = -1;
        public int survivalHamburger = -1;
        public int friedEgg = -1;
        public int cheeseCauliflower = -1;
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

            if (onChangePriceData != null)
                onChangePriceData.Invoke();
        }
        else
        {
            Debug.Log("server connect fail / 가격 정보를 받아올 수 없습니다.");
        }      
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

