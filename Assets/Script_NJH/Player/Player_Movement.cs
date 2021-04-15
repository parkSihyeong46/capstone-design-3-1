using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Manager playerManager;
    public Vector2 playerDirection;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] public bool isMoving;

    private void Start()
    {
        playerManager = GetComponent<Player_Manager>();
    }

    public void Move()
    {
        isMoving = true;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(h, v);

        playerManager.rigidbody.velocity = playerDirection.normalized * moveSpeed;

        isMoving = h != 0 || v != 0;
        playerManager.animator.SetBool("Move", isMoving);

        if (h != 0 || v != 0)
        {
            playerDirection = new Vector2(h, v).normalized;
            playerManager.animator.SetFloat("Last_X", h);
            playerManager.animator.SetFloat("Last_Y", v);
        }
        playerManager.animator.SetFloat("X", h);
        playerManager.animator.SetFloat("Y", v);
    }
}
