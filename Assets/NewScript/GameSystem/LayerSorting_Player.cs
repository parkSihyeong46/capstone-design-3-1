using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSorting_Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    int firstIndex;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        firstIndex = spriteRenderer.sortingOrder;
    }
    void FixedUpdate()
    {
        Debug.Log(spriteRenderer.sortingOrder);
        spriteRenderer.sortingOrder = -(Mathf.RoundToInt(transform.position.y)) + firstIndex;
    }
}
