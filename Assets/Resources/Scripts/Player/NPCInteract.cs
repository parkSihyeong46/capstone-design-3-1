using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : Interact
{
    public Sprite cursorSprite;
    public Sprite npcSprite;
    public string npcName;

    public override void DoInteract(Character character, Item.ItemID itemID)
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (null == cursorSprite)
            return;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (null == cursorSprite)
            return;


    }
}
