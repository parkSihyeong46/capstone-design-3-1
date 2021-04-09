using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputEventManager
{
    private static InputEventManager instance;

    public static InputEventManager Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new InputEventManager();
            }
            return instance;
        }
    }

    public GameObject inventoryObject;
    private InputEventManager() 
    {
        inventoryObject = GameObject.Find("UI").transform.GetChild(1).gameObject;
    }

    public void OpenInventory()
    {
        if (!Inventory.Instance.IsOpen) // 꺼져있다면 
        {
            inventoryObject.SetActive(true);
        }
        else
        {
            inventoryObject.SetActive(false);
        }   
    }
}
