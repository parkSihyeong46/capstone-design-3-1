using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Item_Container))]
public class Item_Container_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        Item_Container container = target as Item_Container;
        if(GUILayout.Button("Clear container"))
        {
            for(int i = 0; i < container.slots.Count; i++)
            {
                container.slots[i].Clear();
            }
        }
        DrawDefaultInspector();
    }
}
