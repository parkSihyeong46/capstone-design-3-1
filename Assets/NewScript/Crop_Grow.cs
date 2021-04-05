using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop_Grow : MonoBehaviour
{
    Animator animator;

    public bool isFullyGrown;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine("CropGrowing");
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
