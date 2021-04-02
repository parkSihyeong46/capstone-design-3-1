using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Marker : MonoBehaviour
{
    [SerializeField] Player_Manager player_Manager;
    [SerializeField]Tilemap_Reader tilemap_Reader;
    [SerializeField] Tilemap targetTilemap;             //마커가 그려질 타일맵
    [SerializeField] TileBase marked;                   //마커에 사용될 스프라이트
    [SerializeField] TileBase nonMarked;                //상호작용 안될때 사용될 스프라이트

    
    Vector3Int oldCellPosition;                 //이전에 마킹되었던 타일
    public Vector3Int markedCellPosition;       //마킹될 타일 위치
    public bool isShow;                         //마커를 표시할지 결정할 값
    float markerMaxDistance = 2f;               //마커가 표시될 최대 거리

    void Update()
    {
        Marking();
        CheckRange();
    }

    void CheckRange()
    {
        Vector2 playerPos = new Vector2(player_Manager.transform.position.x, player_Manager.transform.position.y + 0.5f);   //0.5 더해서 플레이어 중심점 개선
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isShow = Vector2.Distance(playerPos, cameraPos) < markerMaxDistance;
    }

    void Marking()
    {
        //경작 가능할 때 초록색
        //안될 때 빨간색

        markedCellPosition = tilemap_Reader.MousePosToGridPos(Input.mousePosition);
        bool isPlowable = tilemap_Reader.GetMousePosTileData(markedCellPosition);

        if (isShow == true)
        {
            if (isPlowable == true && tilemap_Reader.isEmpty == true)
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
