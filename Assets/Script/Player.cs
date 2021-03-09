using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
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
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move2();
    }

    //void Move()
    //{
    //    float h = Input.GetAxisRaw("Horizontal");
    //    float v = Input.GetAxisRaw("Vertical");
    //    moveVector = new Vector2(h, v);

    //    rigidbody.velocity = moveVector * moveSpeed;

    //    if (animator.GetInteger("hAxisRaw") != h)
    //    {
    //        animator.SetBool("isChange", true);
    //        animator.SetInteger("hAxisRaw", (int)h);
    //    }
    //    else if (animator.GetInteger("vAxisRaw") != v)
    //    {
    //        animator.SetBool("isChange", true);
    //        animator.SetInteger("vAxisRaw", (int)v);
    //    }
    //    else
    //    {
    //        animator.SetBool("isChange", false);
    //    }
    //}

    void Move2()
    {
        isMoving = true;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveVector = new Vector2(h, v);

        rigidbody.velocity = moveVector * moveSpeed;

        isMoving = h != 0 || v != 0;
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

    void UseTool()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 도구 사용
        }
    }
}
