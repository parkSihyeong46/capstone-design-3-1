using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreateTestScripts : MonoBehaviour
{
    public GameObject inventory;

    public void test()
    {
        for(int i = 0; i <15; i ++)
        {
            inventory.GetComponent<Inventory>().AddItem(ItemManager.Instance.GetItem(i));
            Debug.Log(ItemManager.Instance.GetItem(i).ItemName);
        }
    }

    public void deleteTest()
    {
        inventory.GetComponent<Inventory>().DeleteItem(1);
    }
}
