using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//마커 필요한 아이템 선택했을때만 나타나도록 수정(e.g.)호미, 물뿌리개, 씨앗 등)

public class Tilemap_Marker : MonoBehaviour
{
    [SerializeField] Player_Manager player_Manager;
    [SerializeField]Tilemap_Reader tilemap_Reader;
    [SerializeField] Tilemap targetTilemap;     //마커가 그려질 타일맵
    [SerializeField] TileBase marked;           //마커에 사용될 스프라이트
    [SerializeField] TileBase nonMarked;        //상호작용 안될때 사용될 스프라이트
    
    Vector3Int oldCellPosition;             //이전에 마킹되었던 타일
    public Vector3Int markedCellPosition;   //마킹될 타일 위치
    public bool isShow;                     //마커를 표시할지 결정할 값
    public bool isInRange;                  //상호작용 범위에 들어왔는지를 확인할 때 쓰는 값

    void Update()
    {
        Marking();
        CheckRangeAndTool();
    }

    void CheckRangeAndTool()
    {
        Vector2 playerPos = new Vector2(player_Manager.transform.position.x, player_Manager.transform.position.y + 0.5f);   //0.5 더해서 플레이어 중심점 개선(피봇이 bottom이기 때문에)
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        isShow = player_Manager.handItem.ItemId == Item.ItemID.Hoe || player_Manager.handItem.ItemType == Item.ItemTypes.Seed;
        isInRange = Vector2.Distance(playerPos, mousePos) < player_Manager.interactRange;
    }

    void Marking()
    {
        markedCellPosition = tilemap_Reader.MousePosToGridPos(Input.mousePosition);
        bool isPlowable = tilemap_Reader.GetMousePosTileData(markedCellPosition);

        if (isShow == true && isInRange == true)
        {
            if (isPlowable == true && tilemap_Reader.isObjectEmpty == true)
            {
                targetTilemap.SetTile(oldCellPosition, null);       //이전에 마킹된 타일을 null로 바꿈. SetTile : SetTile(타일맵에서 타일의 위치, 배치될 타일)
                targetTilemap.SetTile(markedCellPosition, marked);  //마킹될 위치에 마커 표시
                oldCellPosition = markedCellPosition;               //현재 마킹된 위치는 언제든 바꿀 수 있도록 oldCell로 대입
            }
            else
            {
                targetTilemap.SetTile(oldCellPosition, null);           //이전에 마킹된 타일을 null로 바꿈. SetTile : SetTile(타일맵에서 타일의 위치, 배치될 타일)
                targetTilemap.SetTile(markedCellPosition, nonMarked);   //마킹될 위치에 마커 표시
                oldCellPosition = markedCellPosition;                   //현재 마킹된 위치는 언제든 바꿀 수 있도록 oldCell로 대입
            }
        }
        else
        {
            targetTilemap.SetTile(oldCellPosition, null);
        }
    }
}
