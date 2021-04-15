using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Farming : MonoBehaviour
{
    Player_Manager player_Manager;

    [Header("Get Script")]
    [SerializeField] Tilemap_Reader tilemap_Reader;
    [SerializeField] Tilemap_Marker tilemap_Marker;

    [Header("Plowing")]
    [SerializeField] Tilemap farmingTilemap;    //경작지가 그려질 타일맵
    [SerializeField] TileBase plowedTile;       //경작된 땅 타일이미지(룰타일)

    [Header("Watering")]
    [SerializeField] Tilemap wateringTilemap;   //물뿌려진 땅이 그려질 타일맵
    [SerializeField] TileBase wateringTile;      //물뿌려진 타일이미지(룰타일)

    [Header("Crop")]
    [SerializeField] GameObject tempCrop;       //심어질 작물. 다른거 고르면 바뀌게 해야함

    Vector3Int cellPos;
    bool isPlowed;  //필요할까?

    private void Start()
    {
        player_Manager = GetComponent<Player_Manager>();
    }

    public void Plow()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        bool isPlowalbe = tilemap_Reader.GetMousePosTileData(cellPos);

        //마커 표시됐을때, 땅 위에 아무 오브젝트도 없을 때 경작하도록 함
        if (tilemap_Marker.isShow == true)
        {
            if (isPlowalbe == true && tilemap_Reader.isObjectEmpty == true)
            {
                farmingTilemap.SetTile(cellPos, plowedTile);
            }
        }
    }

    public void Seed()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        //타일이 있을 때, 오브젝트가 없을 때만 모종(타일이 있다는 건 경작지라는 뜻)
        if (tilemap_Marker.isShow == true)
        {
            if (farmingTilemap.GetTile(cellPos) != null && tilemap_Reader.isObjectEmpty == true)
            {
                //씨앗 심기
                //float spread = UnityEngine.Random.Range(0.4f, 0.6f);
                Vector3 cropPos = new Vector3(cellPos.x + 0.5f, cellPos.y, 0);
                Instantiate(tempCrop, cropPos, Quaternion.identity);
            }
        }
    }

    #region 코루틴
    void CropSystem()
    {
        //코루틴으로 자라는 시간 설정해서 일정시간 지나면 다 자라게 함. 아니면 시간 시스템 있으니까 그거 이용해서 얼마 지나면 다 자라도록 함
    }

    IEnumerator CropGrowing()
    {
        //작물이 자라는 코루틴(알아서 잘 자라게 해야하나? 아니면 단계별로 물줘야 자라게 해야하나?)
        //지금은 애니메이션으로 구현해놓음
        return null;
    }

    IEnumerator CropRusting()
    {
        //수확하지 않으면 썩는 코루틴
        return null;
    }
    #endregion

    public void Watering()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        bool isPlowalbe = tilemap_Reader.GetMousePosTileData(cellPos);

        //마커 표시됐을때, 땅 위에 아무 오브젝트도 없을 때 물 뿌리도록 함
        if (tilemap_Marker.isShow == true)
        {
            if (isPlowalbe == true && tilemap_Reader.isObjectEmpty == true)
            {
                wateringTilemap.SetTile(cellPos, wateringTile);
            }
        }

        //시간 지나면 없어지도록 함, 땅은 젖은 땅으로 교체되도록 해야함
    }

    //public void Harvesting()
    //{
    //    RaycastHit2D hit_Crop = Physics2D.Raycast(tilemap_Reader.worldPosition, transform.forward, tilemap_Reader.layerMask_Crop);  //레이캐스트로 받아온 정보
    //    GameObject selectedCrop = hit_Crop.collider.gameObject;             //위 코드에서 받아온 정보 중 게임오브젝트를 게임오브젝트 선언 후 대입
    //}
}
