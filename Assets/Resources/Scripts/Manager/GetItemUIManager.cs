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
        parentObject.transform.SetParent(GameObject.Find("UI").transform);
    }
    public List<GetItemUI> GetItemUIList()
    {
        return getItemUIList;
    }

    public void AddGetItemUIList(Item item)
    {
        getItemUIList.Add(
                GameObject.Instantiate(Resources.Load<GameObject>(UIPrefabPath)).GetComponent<GetItemUI>()  // 오브젝트 생성
                );
        getItemUIList[getItemUIList.Count - 1].GetComponent<Transform>().SetParent(parentObject.transform); // 오브젝트 정리 (부모 등록)
        getItemUIList[getItemUIList.Count - 1].isPrintcount = item.IsPrintCount;        // 수량 출력 여부 등록
        getItemUIList[getItemUIList.Count - 1].SetImageNText(item);                     // 아이템 이미지 등록
        getItemUIList[getItemUIList.Count - 1].ReSizeTextBox(item.ItemName.Length);     // 아이템 이름에 따라 상자 이미지 크기 수정
    }

    public void ResetLocation()
    {
        for(int i = 0; i < getItemUIList.Count; i++)
        {
            getItemUIList[i].transform.localPosition = new Vector3(firstLocation.x, firstLocation.y + (locationDistance * i), firstLocation.z);
        }
    }

    public void PrintUI(Item item)
    {
        if (item == null)
            return;

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

            if (item.ItemImage.name != getItemUIList[i].itemImage.sprite.name)
                continue;

            if (item.IsPrintCount)  // 이미 출력되어 있는 UI 중 (같은 아이템 + 수량 추가 가능) 일 경우 수량 추가
            {
                getItemUIList[i].transform.GetChild(5).GetComponent<Text>().text = (Int32.Parse(getItemUIList[i].transform.GetChild(5).GetComponent<Text>().text) + 1).ToString();
                getItemUIList[i].ResetImageSize();
                return;
            }
        }

        AddGetItemUIList(item);
        getItemUIList[getItemUIList.Count-1].transform.localPosition = new Vector3(firstLocation.x, firstLocation.y + (locationDistance * (getItemUIList.Count - 1)), firstLocation.z);
        return;
    }
}
