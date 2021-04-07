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

    private void Update()
    {
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
    }

    IEnumerator CropGrowing()
    {
        while(isFullyGrown == false)
        {
            yield return new WaitForEndOfFrame();
        }
        isFullyGrown = true;
    }
}
