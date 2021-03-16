using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
public class NoDragScrollRect : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [SerializeField]
    public ScrollRect scrollRect;

    public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.StopMovement();
        scrollRect.enabled = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.enabled = true;
    }
}
