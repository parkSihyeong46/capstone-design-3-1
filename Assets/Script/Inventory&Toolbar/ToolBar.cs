using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    [SerializeField] int toolBarSize = 11;
    int selectedItem;

    public Action<int> onChange;

    private void Update()
    {
        float delta = Input.mouseScrollDelta.y;
        if (delta != 0)
        {
            if(delta < 0)
            {
                selectedItem += 1;
                selectedItem = selectedItem >= toolBarSize ? 0 : selectedItem;
            }
            else
            {
                selectedItem -= 1;
                selectedItem = selectedItem < 0 ? toolBarSize - 1 : selectedItem;
            }
            onChange?.Invoke(selectedItem);
        }
    }

    internal void Set(int id)
    {
        selectedItem = id;
    }
}
