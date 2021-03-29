using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    Character character;
    GameObject gameObject;

    Player_Manager player_Manager;
    Player_Movement player_Movement;
    [SerializeField] MarkerManager markerManager;
    Tilemap_Reader Tilemap_Reader;

    Vector3Int markingPosition;
    bool selectable;

    [SerializeField] float interactRange = 1.5f;
    Vector2 playerPos;

    private void Start()
    {
        player_Manager = GetComponent<Player_Manager>();
        player_Movement = GetComponent<Player_Movement>();
    }

    private void Update()
    {
        CanSelectCheck();
        Marker();
    }

    void Marker()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        markingPosition = //=======================================================================여기서부터 계속(마우스 좌표를 바로 vector3int형으로 바꿀 수 없기때문에 타일맵(그리드).WorldToCell()로 변환해야함)
        markerManager.markedCellPosition = markingPosition;    //마커매니저에 마킹할 좌표 대입
    }

    void CanSelectCheck()
    {
        Vector2 playerPos = transform.position;                                     //플레이어 좌표
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //카메라가 보여주는 화면상에서 마우스가 있는 좌표를 월드포인트로 바꿈
        selectable = Vector2.Distance(playerPos, cameraPos) <= interactRange;       //범위 안에 마우스 포인터가 있으면 true
        markerManager.Show(selectable);
    }

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
}
