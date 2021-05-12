using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelector : MonoBehaviour
{
    [SerializeField] Player_Manager player_Manager;
    SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] toolSprite;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToolSelect()
    {
        if(player_Manager.handItem.ItemType == Item.ItemTypes.Tool)
        {
            this.gameObject.SetActive(true);
            if (player_Manager.handItem.ItemId == Item.ItemID.Axe)
            {
                Debug.Log("플레이어매니저 핸들 아이템ID: " + player_Manager.handItem.ItemId);
                Debug.Log("스프라이트: " + toolSprite[0]);
                spriteRenderer.sprite = toolSprite[0];
            }
            if (player_Manager.handItem.ItemId == Item.ItemID.Pick)
            {
                Debug.Log("플레이어매니저 핸들 아이템ID: " + player_Manager.handItem.ItemId);
                Debug.Log("스프라이트: " + toolSprite[1]);
                spriteRenderer.sprite = toolSprite[1];
            }
            if (player_Manager.handItem.ItemId == Item.ItemID.Hoe)
            {
                Debug.Log("플레이어매니저 핸들 아이템ID: " + player_Manager.handItem.ItemId);
                Debug.Log("스프라이트: " + toolSprite[2]);
                spriteRenderer.sprite = toolSprite[2];
            }
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
}
