using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreateTestScripts : MonoBehaviour
{
    public GameObject inventory;

    public void test()
    {
        inventory.GetComponent<Inventory>().AddItem(ItemManager.Instance.GetItem(0));
        Debug.Log(ItemManager.Instance.GetItem(0).ItemId);
    }
}
