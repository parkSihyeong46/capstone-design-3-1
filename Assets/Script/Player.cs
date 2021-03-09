using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody;
    SpriteRenderer spriteRenderer;

    Vector2 moveVector;

    public float moveSpeed = 5f;

    float
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
        Move();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveVector = new Vector2(h, v);

        rigidbody.velocity = moveVector * moveSpeed;

        if (animator.GetInteger("hAxisRaw") != h)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("hAxisRaw", (int)h);
        }
        else if (animator.GetInteger("vAxisRaw") != v)
        {
            animator.SetBool("isChange", true);
            animator.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            animator.SetBool("isChange", false);
        }
    }

    void CheckObject()
    {

    }
}
