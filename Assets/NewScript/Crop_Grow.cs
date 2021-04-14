using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop_Grow : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    public bool isFullyGrown;

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine("CropGrowing");
    }

    IEnumerator CropGrowing()
    {
        while(isFullyGrown == false)
        {
            yield return new WaitForEndOfFrame();
            if(animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                isFullyGrown = true;
            }
        }
    }
}
