using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScrollBarEvent : MonoBehaviour
{
    [SerializeField]
    private Scrollbar scrollbar;
    [SerializeField]
    private Transform contents;
    public void ScrollLine(bool dir)
    {
        int childCount = contents.childCount;
        if (4 > childCount)
            return;

        if (dir)
        {
            scrollbar.value -= (1.0f / (childCount - 4.0f));
        }
        else
        {
            scrollbar.value += (1.0f / (childCount - 4.0f));
        }
    }
}
