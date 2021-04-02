using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

//땅으로 사용할 타일을 지금은 룰타일로 써서 데이터 비교하고있는데 이렇게 하는것보다
//땅으로 사용할 스프라이트 리스트 같은걸 만들어서 사용하는게 맵 그릴 때 더 효과적일듯
//룰타일로 적용한 타일은 여러개 묶음이 하나로 취급되기때문에 코드상으로 비교하기에는 더 쉽지만 맵 그릴 때 불편함


public class Tilemap_Reader : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;      //하이라키창에서 여기에 타일데이터 넣어줌(Plowable, NotPlowable)
    Dictionary<TileBase, TileData> tileDictionary;  //경작지 타일 리스트

    [SerializeField] LayerMask layerMask;   //오브젝트 판별 레이어
    public bool isEmpty;                    //마우스가 있는 곳에 오브젝트 있는지 확인

    private void Start()
    {
        tileDictionary = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
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

        //여기서 오브젝트 유무 판별
        if (hit) { isEmpty = false; }
        else { isEmpty = true; }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);    //마우스좌표를 Vector3Int형으로 변환 하기위해 WorldToCell(마우스 좌표) 사용
        
        return gridPosition;
    }

    public Vector3 GridPosToMousePos(Vector3Int gridPos)
    {
        Vector3 cellPos = tilemap.CellToWorld(gridPos);
        return cellPos;
    }

    public bool GetMousePosTileData(Vector3Int gridPos)
    {
        TileBase tileBase = tilemap.GetTile(gridPos);   //마우스 좌표가 있는 곳의 타일
        TileData tileData;                              //그 타일의 데이터(참 거짓 여부)
        bool isPlowable = false;

        if (tileBase != null)
        {
            tileData = tileDictionary[tileBase];
        }
        else
        {
            tileData = tileDatas[0];
        }

        if (tileData != null)
        {
            if (tileData == tileDatas[1])       //tileDatas[0] = NotPlowalbe, tileDatas[1] = Plowalbe
            {
                isPlowable = true;
            }
            else
            {
                isPlowable = false;
            }
        }
        return isPlowable;  //경작 가능 불가능 여부 리턴
    }
}
