using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreateTestScripts : MonoBehaviour
{

    public void test()
    {
        for(int i = 0; i <15; i ++)
        {
            Inventory.Instance.AddItem(ItemManager.Instance.GetItem(i));
        }
    }

    public void deleteTest()
    {
        Inventory.Instance.DeleteItem(1);
    }

    public void switchTest()
    {
        Inventory.Instance.SwitchItem(1,20);
    }

    public void useStamina()
    {
        GameObject.Find("staminaBar").GetComponent<StaminaBar>().UseStamina(20);
    }

    public void addStamina()
    {
        GameObject.Find("staminaBar").GetComponent<StaminaBar>().AddStamina(20);
    }
}
