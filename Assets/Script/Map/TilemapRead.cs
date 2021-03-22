using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRead : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;               // 마커의 기준이 될 타일맵
    [SerializeField] List<TileData> tileDatas;      // 심을수 있는지 없는지를 저장할 타일데이터
    Dictionary<TileBase, TileData> dataFromTiles;   // 위 두 데이터를 담을 딕셔너리(구조체같은거)

    private void Start()
    {
        // 새로운 타일데이터 딕셔너리 생성
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);  // 딕셔너리에 타일과 그 타일이 담고있는 정보(Plowable인지 NotPlowable인지)를 추가
            }
        }
    }

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
 
        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position); // 게임 화면상의 Position을 World Position으로 변환. 좌하단(0, 0)
        }
        else
        {
            worldPosition = position;
        }
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition; // 그리드 좌표 반환
    }

    public TileBase GetTileBase(Vector3Int gridPosition, bool mousePosition = false)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        return null;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        return dataFromTiles[tileBase];
    }
}
