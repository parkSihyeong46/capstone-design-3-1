using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player_Manager playerManager;
    public Vector2 playerDirection;

    [SerializeField] float moveSpeed = 5f;

    private void Start()
    {
        playerManager = GetComponent<Player_Manager>();
    }

    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        playerDirection = new Vector2(h, v);

        playerManager.rigidbody.velocity = playerDirection.normalized * moveSpeed;

        playerManager.isMoving = h != 0 || v != 0;    //입력이 되면 true, 아니면 false
        playerManager.animator.SetBool("Move", playerManager.isMoving);

        if (h != 0 || v != 0)
        {
            playerDirection = new Vector2(h, v).normalized;
            playerManager.animator.SetFloat("Last_X", h);
            playerManager.animator.SetFloat("Last_Y", v);
        }
        playerManager.animator.SetFloat("X", h);
        playerManager.animator.SetFloat("Y", v);
    }

    public void Redirection()
    {
        Vector2 redirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        redirection.Normalize();

        //파라미터로 사용할 값(Last_X, Last_Y)을 정수만으로 표현하기 위해서 값 수정 
        if (redirection.x > 0)
        {
            if (redirection.y > 0)
            {
                redirection.x = Mathf.Abs(redirection.x) > Mathf.Abs(redirection.y) ? 1 : 0;
                redirection.y = redirection.x == 0 ? 1 : 0;
            }
            else
            {
                redirection.x = Mathf.Abs(redirection.x) > Mathf.Abs(redirection.y) ? 1 : 0;
                redirection.y = redirection.x == 0 ? -1 : 0;
            }
        }
        else if (redirection.x < 0)
        {
            if (redirection.y > 0)
            {
                redirection.x = Mathf.Abs(redirection.x) > Mathf.Abs(redirection.y) ? -1 : 0;
                redirection.y = redirection.x == 0 ? 1 : 0;
            }
            else
            {
                redirection.x = Mathf.Abs(redirection.x) > Mathf.Abs(redirection.y) ? -1 : 0;
                redirection.y = redirection.x == 0 ? -1 : 0;
            }
        }

        playerManager.animator.SetFloat("Last_X", redirection.x);
        playerManager.animator.SetFloat("Last_Y", redirection.y);
    }
}
