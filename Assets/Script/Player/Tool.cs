using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tool : MonoBehaviour
{
    Player player;
    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeofInteractiveArea = 1.2f;    //상호작용 범위
    [SerializeField] float markerMaxDistance;               //마커 표시 범위
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapRead tilemapRead;
    [SerializeField] CropManager cropManager;
    [SerializeField] TileData plowableTile;

    Vector3Int selectedTilePosition;
    bool selectable;                                        //타일을 선택할 수 있는지 여부

    private void Awake()
    {
        player = GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if (UseToolWorld() == true)    //오브젝트와 상호작용(나무베기, 돌캐기 등)
            {
                return;
            }
            UseToolGrid();                  //그리드와 상호작용(밭갈기, 물뿌리기 등)
        }
    }

    void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;    //마커매니저에 마킹할 좌표 대입
    }

    void SelectTile()
    {
        selectedTilePosition = tilemapRead.GetGridPosition(Input.mousePosition, true);  //타일맵을 불러와서 그리드 좌표에 입력된 마우스좌표 전달 -> 선택된 타일 좌표 결정
    }

    void CanSelectCheck()
    {
        Vector2 playerPos = transform.position;                                     //플레이어 좌표
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //카메라가 보여주는 화면상에서 마우스가 있는 좌표를 월드포인트로 바꿈
        selectable = Vector2.Distance(playerPos, cameraPos) < markerMaxDistance;    //범위 안에 마우스 포인터가 있으면 true
        markerManager.Show(selectable);
    }

    //콜라이더가 있는 물체와 상호작용
    bool UseToolWorld()
    {
        player.animator.SetBool("usingTool", true);

        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;        //플레이어가 바라보는 방향으로 일정 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);   //중심축(플레이어 좌표)을 기준으로 상호작용범위(원)에 들어온 콜라이더

        foreach (Collider2D c in colliders)
        {
            Tool_Hit hit = c.GetComponent<Tool_Hit>();      //Tool_Hit 형 변수 hit에 c(콜라이더) 대입, 도구와 상호작용 할 콜라이더를 hit에 저장

            //콜라이더를 받았을 때 도구와 상호작용할 오브젝트 있으면 true 리턴
            if (hit != null)
            {
                hit.Hit();      // Hit 함수 실행 후 true 리턴
                return true;
            }
        }
        return false;           // 아무것도 없으면 false 리턴
    }

    //그리드(콜라이더 없음. 예를들면 농사지을때 사용하는 타일맵)와 상호작용하는 메소드
    void UseToolGrid()
    {
        if (selectable == true)
        {
            TileBase tileBase = tilemapRead.GetTileBase(selectedTilePosition);

            Debug.Log(tileBase);

            TileData tileData = tilemapRead.GetTileData(tileBase);

            if (cropManager.Check(selectedTilePosition))
            {
                cropManager.Seed(selectedTilePosition);
            }
            else
            {
                cropManager.Plow(selectedTilePosition);
            }
        }
    }
}
