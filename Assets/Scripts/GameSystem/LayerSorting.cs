using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorting : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    bool isEnter;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y));
    }
}
