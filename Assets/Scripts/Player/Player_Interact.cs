using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    Character character;

    [SerializeField] Tilemap_Reader tilemap_Reader;
    [SerializeField] Highlight_Controller highlightController;

    Player_Manager player_Manager;
    Player_Movement player_Movement;
    Player_Farming player_Farming;

    private void Start()
    {
        player_Manager = GetComponent<Player_Manager>();
        player_Movement = GetComponent<Player_Movement>();
        player_Farming = GetComponent<Player_Farming>();
    }

    public Interact Interact()
    {
        StartCoroutine("AnimationCheck");

        RaycastHit2D checkedObject = tilemap_Reader.ObjectCheck(Input.mousePosition);   //마우스 위치에 있는 레이캐스트 정보 가져오기
        Collider2D c = checkedObject.collider;                                          //레이에 맞은 오브젝트의 콜라이더 대입

        if(c != null)
            Debug.Log(c.gameObject);

        if (checkedObject)
        {
            Interact hit = c.GetComponent<Interact>();  //오브젝트가 가지고있는 Interact 스크립트 대입
            return hit;
        }
        return null;
    }

    //애니메이션 재생 및 애니메이션 상태 체크 코루틴(더블클릭 버그 수정 필요함)
    IEnumerator AnimationCheck()
    {
        player_Manager.isAnimation = true;

        //여기에 스위치문 추가

        player_Manager.animator.SetTrigger("Work");     //플레이어 애니메이션 실행(오브젝트에 따라서 수행하는 동작 다르게하기`

        //애니메이션 종료 체크
        while (player_Manager.animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1.7f)
        {
            yield return null;
        }
        player_Manager.isAnimation = false;
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
            case Item.ItemID.Hoe:  // 땅 갈구기
                player_Farming.Plow();
                break;
            case Item.ItemID.WateringCans: // 물주기
                player_Farming.Watering();
                break;
            case Item.ItemID.CauliflowerSeed:   //콜리플라워 씨앗 심기
                player_Farming.Seed();
                break;
            case Item.ItemID.ParsnipSeed:       //파스닙 씨앗 심기
                player_Farming.Seed();
                break;
        }
        player_Manager.UseStamina();   // 스태미나 소모
    }

    //오브젝트 및 아이템 확인 후 상호작용 실행하는 메소드
    void CheckInteract(Item.ItemID itemID)
    {
        Interact interact;

        interact = Interact();
        if (interact != null)
        {
            if ((int)interact.useTool == (int)itemID)   //아이템 코드가 같을 경우
            {
                interact.DoInteract(player_Manager.character);
            }
        }
    }
}
