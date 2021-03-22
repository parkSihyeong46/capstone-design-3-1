using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    Player player;
    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1f;             //
    [SerializeField] float sizeofInteractiveArea = 1.2f;    //상호작용 범위
    [SerializeField] float markerMaxDistance;               //마커 표시 범위
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapRead tilemapRead;
    [SerializeField] CropManager cropManager;

    Vector3Int selectedTilePosition;
    bool selectable;

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
            //도구와 상호작용할 오브젝트가 있다는 의미이므로 상호작용만 할 수 있도록 return 해준다.(한번 클릭 시 땅은 갈지 않고 나무만 베도록 함)
            if (UseToolWorld() == true)
            {
                return;
            }
            UseToolGrid();
        }
    }

    void SelectTile()
    {
        selectedTilePosition = tilemapRead.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 playerPos = transform.position;
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(playerPos, cameraPos) < markerMaxDistance;
        markerManager.Show(selectable);
    }

    void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }    

    //콜라이더가 있는 물체와 상호작용하는 메소드
    bool UseToolWorld()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.animator.SetBool("usingTool", true);

        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;        //
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);   // 원을 기준으로 상호작용범위에 들어온 콜라이더

        foreach (Collider2D c in colliders)
        {
            // 도구와 상호작용 할 콜라이더를 hit에 저장
            Tool_Hit hit = c.GetComponent<Tool_Hit>();      //Tool_Hit 형 변수 hit에 c(콜라이더) 대입

            // 콜라이더를 받았을 때 도구와 상호작용할 오브젝트 있으면 true 리턴
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
        if (selectable)
        {
            //마킹된 타일에 심기
            cropManager.Plow(selectedTilePosition);
        }
    }
}
