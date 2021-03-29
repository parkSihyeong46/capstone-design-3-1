using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    Player_Movement player_Movement;
    Player_Interact player_Interact;

    SpriteRenderer spriteRenderer;
    public Animator animator;
    public Rigidbody2D rigidbody;

    private void Start()
    {
        player_Movement = GetComponent<Player_Movement>();
        player_Interact = GetComponent<Player_Interact>();

        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        player_Movement.Move();
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && player_Movement.isMoving == false)
        {
            player_Interact.Interact();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)    //현재 애니메이션이 끝나면 false로 바꿔서 반복 안함
        {
            animator.SetBool("usingTool", false);
        }
    }
}
