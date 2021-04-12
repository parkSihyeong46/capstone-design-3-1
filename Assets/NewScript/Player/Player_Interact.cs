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
    [SerializeField] float interactRange = 1.5f;

    private void Start()
    {
        player_Manager = GetComponent<Player_Manager>();
        player_Movement = GetComponent<Player_Movement>();
    }

    void Update()
    {
        Highlight();
    }

    //플레이어 앞 1칸으로 상호작용 범위 축소, 현재 마우스 포인터가 위치한 곳의 오브젝트를 받아오도록 수정하기 >> 그러면 작물 하나하나 수확하는것 가능할 듯
    public Interact Interact()
    {
        //기존 오버랩서클 >> 마우스 위치한 곳에 레이캐스트(또는 박스캐스트)로 오브젝트 받기

        player_Manager.animator.SetBool("usingTool", true);     //플레이어 애니메이션 실행(오브젝트에 따라서 수행하는 동작 다르게하기)

        RaycastHit2D checkedObject = tilemap_Reader.ObjectCheck(Input.mousePosition);   //마우스 위치에 있는 레이캐스트 정보 가져오기
        Collider2D c = checkedObject.collider;                                          //레이에 맞은 오브젝트의 콜라이더 대입

        Debug.Log(c.gameObject);

        if (checkedObject)
        {
            Interact hit = c.GetComponent<Interact>();

            return hit;
        }

        return null;
    }

    //작동을 안하네... 상호작용 범위 줄이면 필요 없을 수 있음
    public void Highlight()
    {
        Vector2 position = player_Manager.rigidbody.position + player_Movement.playerDirection;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactRange);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();

            //Debug.Log(hit);

            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }
        highlightController.Hide();
    }
}
