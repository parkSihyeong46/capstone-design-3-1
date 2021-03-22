using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerManager : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;     //마커가 그려질 타일맵
    [SerializeField] TileBase tile;             //마커에 사용될 스프라이트
    public Vector3Int markedCellPosition;       //마킹될 위치
    Vector3Int oldCellPosition;                 //이건 뭔지 모르겠다
    bool show;

    private void Update()
    {
        //선택할 수 없는 오브젝트면 마크 표시 안함
        if(show == false) { return; }
        targetTilemap.SetTile(oldCellPosition, null);       //이건 뭔지 모르겠다
        targetTilemap.SetTile(markedCellPosition, tile);    //마킹될 위치에 마커 표시
        oldCellPosition = markedCellPosition;               //이건 뭔지 모르겠다
    }

    internal void Show(bool selectable)
    {
        show = selectable;
        targetTilemap.gameObject.SetActive(show);
    }
}
