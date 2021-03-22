﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Crops
{

}

public class CropManager : MonoBehaviour
{
    [SerializeField] TileBase plowed;
    [SerializeField] TileBase seeded;
    [SerializeField] Tilemap targetTilemap;

    Dictionary<Vector2Int, Crops> crops; // 여기에는 심어질 농작물의 종류와 위치가 들어감

    private void Start()
    {
        crops = new Dictionary<Vector2Int, Crops>();
    }

    public void Plow(Vector3Int position)
    {
        //이미 심어져있으면 리턴
        if (crops.ContainsKey((Vector2Int)position))
        {
            return;
        }

        //심을 수 있으면 심는 함수 실행
        CreatePlowedTile(position);
    }

    void CreatePlowedTile(Vector3Int position)
    {
        Crops crop = new Crops();
        crops.Add((Vector2Int)position, crop);

        targetTilemap.SetTile(position, plowed);
    }
}