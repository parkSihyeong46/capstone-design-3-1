using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ToolManager
{
    private static ToolManager instance;
    private const string toolDataPath = "Data/toolData";
    private TextAsset textAsset;
    private List<Tool> toolDataList = new List<Tool>();

    public static ToolManager Instance
    {
        get
        {
            if (null == instance)
            {
                //게임 인스턴스가 없다면 하나 생성해서 넣어준다.
                instance = new ToolManager();
            }
            return instance;
        }
    }

    private ToolManager()
    {
        textAsset = Resources.Load<TextAsset>(toolDataPath);
        string[] line = textAsset.text.Substring(0, textAsset.text.Length - 1).Split('\n');
        for (int i = 1; i < line.Length; i++)
        {
            string[] row = line[i].Split('\t');

            try
            {
                toolDataList.Add(new Tool((Item.ItemID)Enum.Parse(typeof(Item.ItemID), row[0]),
                    row[1],
                    Int32.Parse(row[2]),
                    Int32.Parse(row[3])
                    ));
            }
            catch
            {
                toolDataList.Add(null);
            }
        }
    }

    public Tool GetTool(Item item)
    {
        if (item == null)
            return null;

        if (item.ItemType != Item.ItemTypes.Tool)
            return null;

        switch (item.ItemId)
        {
            case Item.ItemID.Axe:
            case Item.ItemID.Pick:
            case Item.ItemID.Hoe:
            case Item.ItemID.WateringCans:
                foreach(Tool findTool in toolDataList)
                { 
                    if(findTool.ItemId == item.ItemId)
                    {
                        return findTool;
                    }
                }
                break;
            default:
                return null;
        }

        return null;
    }
}
