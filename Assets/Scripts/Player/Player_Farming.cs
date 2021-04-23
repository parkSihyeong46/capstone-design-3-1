using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Farming : MonoBehaviour
{
    Player_Manager player_Manager;
    Player_Interact player_Interact;

    [Header("Get Script")]
    [SerializeField] Tilemap_Reader tilemap_Reader;
    [SerializeField] Tilemap_Marker tilemap_Marker;

    [Header("Plowing")]
    [SerializeField] Tilemap farmingTilemap;    //경작지가 그려질 타일맵
    [SerializeField] TileBase plowedTile;       //경작된 땅 타일이미지(룰타일)

    [Header("Watering")]
    [SerializeField] Tilemap wateringTilemap;   //물뿌려진 땅이 그려질 타일맵
    [SerializeField] TileBase wateringTile;     //물뿌려진 타일이미지(룰타일)

    [Header("Crop")]
    [SerializeField] GameObject selectedCrop;   //심어질 작물. 다른거 고르면 바뀌게 해야함

    Vector3Int cellPos;

    bool isPlowed;  //필요할까?

    private void Start()
    {
        player_Manager = GetComponent<Player_Manager>();
        player_Interact = GetComponent<Player_Interact>();
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
                player_Interact.StartCoroutine("AnimationCheck", "Work");
                farmingTilemap.SetTile(cellPos, plowedTile);
            }
        }
    }

    public void Seed()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        //타일이 있을 때, 오브젝트가 없을 때만 모종(타일이 있다는 건 경작된 땅이라는 뜻)
        if (tilemap_Marker.isShow == true)
        {
            if (farmingTilemap.GetTile(cellPos) != null && tilemap_Reader.isObjectEmpty == true)
            {
                //씨앗 심기
                Vector3 cropPos = new Vector3(cellPos.x + 0.5f, cellPos.y, 0);
                Instantiate(selectedCrop, cropPos, Quaternion.identity);
            }
        }
    }

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
}
