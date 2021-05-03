using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player_Farming : MonoBehaviour
{
    [SerializeField] CropManager cropManager;

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

    Vector3Int cellPos;

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
        if (tilemap_Marker.isShow == true && tilemap_Marker.isInRange == true)
        {
            if (isPlowalbe == true && tilemap_Reader.isObjectEmpty == true)
            {
                player_Manager.RunAnimation("Work");
                farmingTilemap.SetTile(cellPos, plowedTile);
            }
        }
    }

    public void Seed()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        GameObject selectedCrop = cropManager.CropSelect();

        if (null == selectedCrop)
            return;

        //타일이 있을 때, 오브젝트가 없을 때만 모종(타일이 있다는 건 경작된 땅이라는 뜻)
        if (farmingTilemap.GetTile(cellPos) != null && tilemap_Reader.isObjectEmpty == true)
        {
            if (tilemap_Marker.isShow == true && tilemap_Marker.isInRange == true)
            {
                //씨앗 심기
                Vector3 cropPos = new Vector3(cellPos.x + 0.5f, cellPos.y, 0);
                Instantiate(selectedCrop, cropPos, Quaternion.identity);
                Inventory.Instance.DeleteItemId(player_Manager.handItem.ItemId); // 심은 씨앗 인벤에서 제거
            }
        }
    }

    public void Watering()
    {
        cellPos = tilemap_Marker.markedCellPosition;

        bool isPlowalbe = tilemap_Reader.GetMousePosTileData(cellPos);

        //상호작용 가능한 범위일 때, 땅 위에 아무 오브젝트도 없을 때 물 뿌리도록 함
        if (tilemap_Marker.isInRange == true)
        {
            if (isPlowalbe == true && tilemap_Reader.isObjectEmpty == true)
            {
                wateringTilemap.SetTile(cellPos, wateringTile);
            }
        }
    }
}
