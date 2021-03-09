using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    Player player;
    Rigidbody2D rigidbody;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeofInteractiveArea = 1.2f;

    private void Awake()
    {
        player = GetComponent<Player>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseTool();
        }
    }

    private void UseTool()
    {
        Vector2 position = rigidbody.position + player.directionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeofInteractiveArea);

        foreach(Collider2D c in colliders)
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
