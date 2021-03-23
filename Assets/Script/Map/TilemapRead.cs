using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRead : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;               // 데이터를 받아올 타일맵
    [SerializeField] List<TileData> tileDatas;      // 심을수 있는지 없는지를 저장할 타일데이터
    Dictionary<TileBase, TileData> dataFromTiles;   // 위 두 데이터를 담을 딕셔너리(구조체같은거)

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();   //새로운 타일데이터 딕셔너리 생성
        foreach (TileData tileData in tileDatas)        //저장된 타일데이터들에서 타일데이터 중
        {
            foreach (TileBase tile in tileData.tiles)   //그 타일데이터의 타일
            {
                Debug.Log("타일: " + tile + "    타일데이터: " + tileData);
                dataFromTiles.Add(tile, tileData);  //딕셔너리에 타일과 그 타일이 담고있는 정보(Plowable인지 NotPlowable인지)를 추가
            }
        }
    }

    //Vector3Int는 정수형 값을 벡터값으로 사용함
    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition) //마우스가 위치해있을때만 실행되도록 하기위해 
    {
        Vector3 worldPosition;
 
        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position); //게임 화면상의 Position을 World Position으로 변환. 좌하단(0, 0)
        }
        else
        {
            worldPosition = position;
        }
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition; //그리드 좌표 반환
    }

    public TileBase GetTileBase(Vector3Int gridPosition, bool mousePosition = false)
    {
        TileBase tile = tilemap.GetTile(gridPosition);

        return null;
    }

    public TileData GetTileData(TileBase tileBase)
    {
        return dataFromTiles[tileBase]; //들어온 타일의 데이터를 반환
    }
}
