using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_InteractController : MonoBehaviour
{
    Player player;
    Character character;

    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeofInteractiveArea = 1.2f;
    [SerializeField] float markerMaxDistance = 2f;
    [SerializeReference] Highlight_Controller highlightController;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapRead tilemapRead;
    [SerializeField] CropManager cropManager;
    [SerializeField] TileData plowableTile;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        player = GetComponent<Player>();
        character = GetComponent<Character>();

        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Check();
        SelectTile();
        CanSelectCheck();
        Marker();

        if (Input.GetMouseButtonDown(0) && player.isMoving == false)
        {
            if (UseToolWorld() == true)    //오브젝트와 상호작용(나무베기, 돌캐기 등)
            {
                return;
            }
            UseToolGrid();                 //그리드와 상호작용(밭갈기, 물뿌리기 등)
        }
        else if (player.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)    //현재 애니메이션이 끝나면 false로 바꿔서 반복 안함
        {
            player.animator.SetBool("usingTool", false);
        }
    }

    public void Check()
    {
        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }
        highlightController.Hide();
    }

    void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;    //마커매니저에 마킹할 좌표 대입
    }

    void SelectTile()
    {
        selectedTilePosition = tilemapRead.GetGridPosition(Input.mousePosition, true);  //타일맵을 불러와서 그리드 좌표에 입력된 마우스좌표 전달 -> 선택된 타일 좌표 결정
    }

    bool UseToolWorld()
    {
        player.animator.SetBool("usingTool", true);

        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;        //플레이어가 바라보는 방향으로 일정 범위
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);   //중심축(플레이어 좌표)을 기준으로 상호작용범위(원)에 들어온 콜라이더

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();      //Tool_Hit 형 변수 hit에 c(콜라이더) 대입, 도구와 상호작용 할 콜라이더를 hit에 저장

            //콜라이더를 받았을 때 도구와 상호작용할 오브젝트 있음
            if (hit != null)
            {
                hit.Interact(character);    //c가 가지고 있는 Hit 함수 실행(나무면 나무, 돌이면 돌 등) 후 true 리턴
                return true;
            }
        }
        return false;                       // 아무것도 없으면 false 리턴
    }

    void UseToolGrid()
    {
        if (selectable == true)
        {
            //TileBase tileBase = tilemapRead.GetTileBase(selectedTilePosition);  //선택된 타일베이스
            //TileData tileData = tilemapRead.GetTileData(tileBase);

            TileData tileData = tilemapRead.SelectedTile(selectedTilePosition);

            if (cropManager.Check(selectedTilePosition))
            {
                cropManager.Plow(selectedTilePosition);
            }
        }
    }

    void CanSelectCheck()
    {
        Vector2 playerPos = transform.position;                                     //플레이어 좌표
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //카메라가 보여주는 화면상에서 마우스가 있는 좌표를 월드포인트로 바꿈
        selectable = Vector2.Distance(playerPos, cameraPos) < markerMaxDistance;    //범위 안에 마우스 포인터가 있으면 true
        markerManager.Show(selectable);
    }
}
