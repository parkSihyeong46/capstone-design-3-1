using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRead : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);
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
