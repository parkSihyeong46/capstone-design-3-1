using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Farm : MonoBehaviour
{
    [SerializeField] Tilemap checkTilemap;  //경작여부 체크할 타일맵
    [SerializeField] Tilemap targetTilemap; //경작지가 그려질 타일맵

    void CheckTile()
    {
        GetTileBase();
    }

    void GetTileBase()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int girdPosition = (Vector3Int)checkTilemap.WorldToCell(worldPosition);

        TileBase tile = checkTilemap.GetTile(girdPosition);

        Debug.Log(tile);
    }


    //체크할 타일맵의 모든 타일을 검사?
    //1. 마우스 위치를 그리드포지션으로 반환
    //2. 그 위치(그리드포지션)의 타일을 가져옴
    //3. 그 타일은 체크타일맵의 타일임
    //4. 그 좌표상에는 체크타일맵 말고 다른 오브젝트나 타일이 없어야함
}
