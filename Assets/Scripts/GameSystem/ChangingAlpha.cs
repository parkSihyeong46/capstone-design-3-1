using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingAlpha : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool isEnter;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isEnter = true;
        Color color = spriteRenderer.color;
        color.a = 1f;
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("SmoothAlpha", color);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isEnter = false;
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine("SmoothAlpha", color);
        }
    }

    //알파값 변경 코루틴
    IEnumerator SmoothAlpha(Color color)
    {
        if (isEnter == true)     //콜라이더에 들어왔을 경우
        {
            while (color.a >= 0.5f)
            {
                color.a -= 0.05f;
                spriteRenderer.color = color;
                yield return null;
            }
        }
        if (isEnter == false)   //콜라이더에서 나갔을 경우
        {
            while (color.a <= 1f)
            {
                color.a += 0.05f;
                spriteRenderer.color = color;
                yield return null;
            }
        }
    }
}
