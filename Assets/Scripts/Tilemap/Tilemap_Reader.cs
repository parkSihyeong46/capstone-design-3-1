using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tilemap_Reader : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;      //하이라키창에서 여기에 타일데이터 넣어줌(Plowable, NotPlowable)
    Dictionary<TileBase, TileData> tileDictionary;  //경작지 타일 딕셔너리

    public LayerMask layerMask_Object;  //오브젝트 판별 레이어
    public LayerMask layerMask_Crop;    //작물 판별 레이어
    public bool isObjectEmpty;          //마우스가 있는 곳에 오브젝트 있는지 확인
    //public bool isCropEmpty;            //마우스가 있는 곳에 작물 있는지 확인

    public Vector3 worldPosition;

    private void Start()
    {
        tileDictionary = new Dictionary<TileBase, TileData>();
        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)   //여기서 tile은 Plowable 파일에 넣은 타일베이스
            {
                tileDictionary.Add(tile, tileData);     //딕셔너리에 타일과 그 타일이 담고있는 정보(Plowable인지 NotPlowable인지)를 추가
            }
        }
    }

    //Vector3형을 Vector3Int형으로 변환하는 메소드(타일맵 마커 등에 사용)
    public Vector3Int MousePosToGridPos(Vector3 mousePos)
    {
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit_Object = Physics2D.Raycast(worldPosition, transform.forward, 15f, layerMask_Object);
        //RaycastHit2D hit_Crop = Physics2D.Raycast(worldPosition, transform.forward, 15f, layerMask_Crop);

        //오브젝트 유무 판별
        if (hit_Object) { isObjectEmpty = false; }
        else { isObjectEmpty = true; }

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);    //마우스좌표를 Vector3Int형으로 변환 하기위해 WorldToCell(마우스 좌표) 사용
        
        return gridPosition;
    }
    
    //Vector3Int형을 Vector3형으로 변환하는 메소드(작물 심기 등에 사용)
    public Vector3 GridPosToMousePos(Vector3Int gridPos)
    {
        Vector3 cellPos = tilemap.CellToWorld(gridPos);
        return cellPos;
    }

    //오브젝트 판별 메소드
    public RaycastHit2D ObjectCheck(Vector3 mousePos)
    {
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit_Object = Physics2D.Raycast(worldPosition, transform.forward, 15f, layerMask_Object);
        return hit_Object;
    }

    //경작 여부 판별 메소드
    public bool GetMousePosTileData(Vector3Int gridPos)
    {
        TileBase mouseTileBase = tilemap.GetTile(gridPos);   //마우스 좌표가 있는 곳의 타일
        TileData mouseTileData;                              //그 타일의 데이터(참 거짓 여부)
        bool isPlowable = false;

        if (mouseTileBase != null)  //마우스 위치에 타일이 있을 때
        {
            mouseTileData = tileDictionary[mouseTileBase];  //타일데이터는 mouseTileBase를 키값으로 사용해 딕셔너리에 저장되어있는 데이터 대입
        }
        else
        {
            mouseTileData = tileDatas[0];
        }

        if (mouseTileData != null)
        {
            if (mouseTileData == tileDatas[1])       //tileDatas[0] = NotPlowalbe, tileDatas[1] = Plowalbe
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
