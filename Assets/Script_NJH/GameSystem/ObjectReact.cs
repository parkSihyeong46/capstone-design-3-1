using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReact : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //(플레이어, NPC, 동물 등)캐릭터 레이어(10번째 레이어)가 닿았을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            animator.SetTrigger("reactTrigger");
        }
    }
}
