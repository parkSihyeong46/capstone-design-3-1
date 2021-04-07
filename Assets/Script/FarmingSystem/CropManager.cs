using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{

}

public class CropManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;       //경작한 땅 스프라이트
    [SerializeField] TileBase seeded;       //씨앗 스프라이트
    [SerializeField] Tilemap targetTilemap; //위의 것들을 그려질 타일맵

    Dictionary<Vector2Int, Crops> crops; // 여기에는 심어질 농작물의 종류와 위치가 들어감

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crops>();
    }

    public bool Check(Vector3Int position)
    {
        return crops.ContainsKey((Vector2Int)position);
    }

    public void Plow(Vector3Int position)
    {
        //이미 심어져있으면 리턴
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        //경작한 땅 생성
        CreatePlowedTile(position);
    }

    public void Seed(Vector3Int position)
    {
        targetTilemap.SetTile(position, seeded);
    }

    void CreatePlowedTile(Vector3Int position)
    {
        Crops crop = new Crops();
        crops.Add((Vector2Int)position, crop);

        targetTilemap.SetTile(position, plowed);
    }
}
