﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    Player player;
    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeofInteractiveArea = 1.2f;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] TilemapRead tilemapRead;
    [SerializeField] float maxDistance;

    Vector3Int selectedTilePosition;
    bool selectable;

    private void Awake()
    {
        player = GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SelectTile();
        CanSelectCheck();
        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    void SelectTile()
    {
        selectedTilePosition = tilemapRead.GetGridPosition(Input.mousePosition, true);
    }

    void CanSelectCheck()
    {
        Vector2 playerPos = transform.position;
        Vector2 cameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(playerPos, cameraPos) < maxDistance;
        markerManager.Show(selectable);
    }

    void Marker()
    {
        markerManager.markedCellPosition = selectedTilePosition;
    }    

    public void UseTool()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        player.animator.SetBool("usingTool", true);

        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);

        foreach (Collider2D c in colliders)
        {
            // 도구에 맞을 콜라이더를 hit에 저장
            Tool_Hit hit = c.GetComponent<Tool_Hit>();
            if (hit != null)
            {
                hit.Hit();
                break;
            }
        }
    }
}