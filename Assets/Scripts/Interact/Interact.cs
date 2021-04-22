using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public Item.ItemID itemID = Item.ItemID.Hand;
        
    public virtual void DoInteract(Character character, Item.ItemID itemID)
    {
        //각각 오버라이드로 사용
    }
}
