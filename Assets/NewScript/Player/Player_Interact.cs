using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    Character character;

    [SerializeReference] Highlight_Controller highlightController;

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

    //상호작용 범위 축소, 현재 마우스 포인터가 위치한 곳의 오브젝트를 받아오도록 수정하기 >> 그러면 작물 하나하나 수확하는것 가능할 듯
    public void Interact()
    {
        player_Manager.animator.SetBool("usingTool", true);

        Vector2 position = player_Manager.rigidbody.position + player_Movement.playerDirection * interactRange;        //플레이어가 바라보는 방향으로 일정 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, interactRange);   //중심축(플레이어 좌표)을 기준으로 상호작용범위(원)에 들어온 콜라이더

        foreach (Collider2D c in colliders)
        {
            Interact hit = c.GetComponent<Interact>();      //Tool_Hit 형 변수 hit에 c(콜라이더) 대입, 도구와 상호작용 할 콜라이더를 hit에 저장

            //콜라이더를 받았을 때 도구와 상호작용할 오브젝트 있음
            if (hit != null)
            {
                hit.DoInteract(character);    //c가 가지고 있는 Hit 함수 실행(나무면 나무, 돌이면 돌 등) 후 true 리턴
            }
        }
    }

    //작동을 안하네...
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
