using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Reader : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;      //하이라키창에서 여기에 타일데이터 넣어줌
    Dictionary<TileBase, TileData> tileDictionary;  //경작지 타일 리스트

    [SerializeField] LayerMask layerMask;   //오브젝트 판별 레이어
    public bool isEmpty;                    //마우스가 있는 곳에 오브젝트 있는지 확인

    private void Start()
    {
        tileDictionary = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)        //저장된 타일데이터들에서 타일데이터 중
        {
            foreach (TileBase tile in tileData.tiles)   //그 타일데이터의 타일
            {
                tileDictionary.Add(tile, tileData);     //딕셔너리에 타일과 그 타일이 담고있는 정보(Plowable인지 NotPlowable인지)를 추가
            }
        }
    }

    public Vector3Int MousePosToGridPos(Vector3 mousePos)
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPosition, transform.forward, 15f, layerMask);

        if (hit) { isEmpty = false; }
        else { isEmpty = true; }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);    //마우스좌표를 Vector3Int형으로 변환 하기위해 WorldToCell(마우스 좌표) 사용
        return gridPosition;
    }

    public bool GetMousePosTileData(Vector3Int gridPos)
    {
        TileBase tileBase = tilemap.GetTile(gridPos);   //마우스 좌표가 있는 곳의 타일
        TileData tileData = tileDictionary[tileBase];   //그 타일의 데이터(참 거짓 여부)

        bool isPlowable = true;

        if (tileData != null)
        {
            if (tileData == tileDatas[0])       //tileDatas[0] = NotPlowalbe, tileDatas[1] = Plowalbe
            {
                isPlowable = false;
            }
            else
            {
                isPlowable = true;
            }
        }
        return isPlowable;
    }
}
