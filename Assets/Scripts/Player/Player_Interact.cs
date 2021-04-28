using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    
    Character character;

    [SerializeField] Tilemap_Reader tilemap_Reader;
    [SerializeField] Tilemap_Marker tilemap_Marker;
    [SerializeField] Highlight_Controller highlightController;

    Player_Manager player_Manager;
    Player_Movement player_Movement;
    Player_Farming player_Farming;

    float animationTime;

    private void Start()
    {
        player_Manager = GetComponent<Player_Manager>();
        player_Movement = GetComponent<Player_Movement>();
        player_Farming = GetComponent<Player_Farming>();
    }

    public Interact Interact()
    {
        RaycastHit2D checkedObject = tilemap_Reader.ObjectCheck(Input.mousePosition);   //마우스 위치에 있는 레이캐스트 정보 가져오기
        Collider2D c = checkedObject.collider;                                          //레이에 맞은 오브젝트의 콜라이더 대입

        if(c != null)
            Debug.Log(c.gameObject);

        if (checkedObject)
        {
            Interact hit = c.GetComponent<Interact>();  //오브젝트가 가지고있는 Interact 스크립트 대입
            return hit; //오브젝트의 Interact 리턴
        }
        return null;
    }

    public void UseTool()
    {
        if (player_Manager.handItem == null)   // 아이템을 들고있을 때만 수행하도록
            return;

        if (player_Manager.handItem.ItemType != Item.ItemTypes.Tool &&
            player_Manager.handItem.ItemType != Item.ItemTypes.Seed)   // 들고있는 아이템이 tool이나 seed가 아니면 리턴
            return;

        switch (player_Manager.handItem.ItemId)
        {
            case Item.ItemID.Axe: // 나무캐기
                CheckInteract(Item.ItemID.Axe);
                break;
            case Item.ItemID.Pick: // 돌부수기
                CheckInteract(Item.ItemID.Pick);
                break;
            case Item.ItemID.Hoe:  // 땅 갈기
                CheckInteract(Item.ItemID.Hoe);
                player_Farming.Plow();
                break;
            case Item.ItemID.WateringCans: // 물주기
                CheckInteract(Item.ItemID.WateringCans);
                player_Farming.Watering();
                break;
            case Item.ItemID.CauliflowerSeed:   //콜리플라워 씨앗 심기
                CheckInteract(Item.ItemID.CauliflowerSeed);
                player_Farming.Seed();
                break;
            case Item.ItemID.ParsnipSeed:       //파스닙 씨앗 심기
                CheckInteract(Item.ItemID.ParsnipSeed);
                player_Farming.Seed();
                break;
        }
        player_Manager.UseStamina();   // 스태미나 소모
    }

    //오브젝트 및 아이템 확인 후 상호작용 실행하는 메소드
    void CheckInteract(Item.ItemID itemID)
    {
        Debug.Log(itemID);

        Interact interact;
        interact = Interact();

        if (interact != null && tilemap_Marker.isShow == true)
        {
            interact.DoInteract(player_Manager.character, itemID);
        }
    }
}
