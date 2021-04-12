using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GetItemUIManager
{
    private static GetItemUIManager instance;
    private GameObject parentObject;
    private List<GetItemUI> getItemUIList = new List<GetItemUI>();

    private const string UIPrefabPath = "Prefabs/UI/GetItemUI";

    private Vector3 firstLocation = new Vector3(100, 120, 0);
    private const int locationDistance = 80;

    public static GetItemUIManager Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new GetItemUIManager();
            }
            return instance;
        }
    }

    private GetItemUIManager()
    {
        parentObject = new GameObject("GetItemUI");
        parentObject.transform.parent = GameObject.Find("UI").transform;
    }

    public void AddGetItemUIList(Item item)
    {
        getItemUIList.Add(
                GameObject.Instantiate(Resources.Load<GameObject>(UIPrefabPath)).GetComponent<GetItemUI>()
                );
        getItemUIList[getItemUIList.Count - 1].GetComponent<Transform>().parent = parentObject.transform;
        getItemUIList[getItemUIList.Count - 1].isPrintcount = item.IsPrintCount;
        getItemUIList[getItemUIList.Count - 1].SetImageNText(item);
    }

    public void DeleteNullUIList()
    {
        for(int i = 0; i < getItemUIList.Count;)
        {
            if (null == getItemUIList[i])
            {
                getItemUIList.RemoveAt(i);
            }
            else
                i++;
        }
    }
    public void PrintUI(Item item)
    {
        if (item == null)
            return;

        //DeleteNullUIList();

        if (0 == getItemUIList.Count)
        {
            AddGetItemUIList(item);
            getItemUIList[0].transform.localPosition = firstLocation;
            return;
        }

        for(int i = 0; i < getItemUIList.Count; i++)
        {
            if (getItemUIList[i] == null)
                continue;

            if (item.ItemImage != getItemUIList[i].transform.GetChild(3).GetComponent<Sprite>())
                continue;

            if (item.IsPrintCount)
            {
                getItemUIList[i].transform.GetChild(5).GetComponent<Text>().text = (Int32.Parse(getItemUIList[i].transform.GetChild(5).GetComponent<Text>().text) + 1).ToString();
                getItemUIList[i].ResetImageSize();
                return;
            }
            else
            {
                AddGetItemUIList(item);
                getItemUIList[i].transform.localPosition = new Vector3(firstLocation.x, firstLocation.y + (locationDistance * i), firstLocation.z);
                return;
            }
        }
    }
}
