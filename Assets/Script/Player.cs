﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Tool tool;

    public Animator animator;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    [Header("Player Vector")]
    Vector2 moveVector;
    public Vector2 directionVector;

    [Header("Player Stat")]
    public float moveSpeed = 5f;


    [Header("Player Status")]
    bool isMoving;

    void Start()
    {
        tool = GetComponent<Tool>();

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {     
        tool.UseTool();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        isMoving = true;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveVector = new Vector2(h, v);

        rigidbody.velocity = moveVector * moveSpeed;

        isMoving = h != 0 || v != 0;        // 키입력 하거나 끝나면 true false 전환
        animator.SetBool("isMoving", isMoving);

        if (h != 0 || v != 0)
        {
            directionVector = new Vector2(h, v).normalized;
            animator.SetFloat("lastHorizontal", h);
            animator.SetFloat("lastVertical", v);
        }

        animator.SetFloat("horizontal", h);
        animator.SetFloat("vertical", v);
    }
}
