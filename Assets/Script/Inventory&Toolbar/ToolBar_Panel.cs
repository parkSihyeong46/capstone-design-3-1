using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar_Panel : Item_Panel
{
    [SerializeField] ToolBar toolBar;

    private void Start()
    {
        Init();
        toolBar.onChange += Highlight;
        Highlight(0);
    }

    public override void OnClick(int id)
    {
        toolBar.Set(id);
        Highlight(id);
    }

    int currentSelectedItem;

    public void Highlight(int id)
    {
        buttons[currentSelectedItem].Highlight(false);
        currentSelectedItem = id;
        buttons[currentSelectedItem].Highlight(true);
    }
}
