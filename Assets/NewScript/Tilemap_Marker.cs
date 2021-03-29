using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Marker : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;     //마커가 그려질 타일맵
    [SerializeField] TileBase marked;           //마커에 사용될 스프라이트
    [SerializeField] TileBase nonMarked;        //상호작용 안될때 사용될 스프라이트
    public Vector3Int markedCellPosition;       //마킹될 타일 위치
    Vector3Int oldCellPosition;                 //이전에 마킹되었던 타일
    bool isShow;                                //마커를 표시할지 결정할 값

    private void Update()
    {
        Marking();
    }

    internal void Show(bool selectable)
    {
        isShow = selectable;
        targetTilemap.gameObject.SetActive(isShow);
    }

    void Marking()
    {
        if (isShow == false) { return; }                    //마킹할 수 없으면 마커 표시 안함
        targetTilemap.SetTile(oldCellPosition, null);       //이전에 마킹된 타일을 null로 바꿈. SetTile : SetTile(타일맵에서 타일의 위치, 배치될 타일)
        targetTilemap.SetTile(markedCellPosition, marked);  //마킹될 위치에 마커 표시
        oldCellPosition = markedCellPosition;               //현재 마킹된 위치는 언제든 바꿀 수 있도록 oldCell로 대입
    }
}
