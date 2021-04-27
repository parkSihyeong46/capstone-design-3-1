using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CropManager : MonoBehaviour
{
    [SerializeField] Player_Manager player_Manager;

    [SerializeField] GameObject[] crops;

    public GameObject CropSelect()
    {
        GameObject selectedCrop = null;

        if(player_Manager.handItem.ItemType == Item.ItemTypes.Seed)
        {
            if(player_Manager.handItem.ItemId == Item.ItemID.ParsnipSeed)
            {
                selectedCrop = crops[0];
            }
            if (player_Manager.handItem.ItemId == Item.ItemID.CauliflowerSeed)
            {
                selectedCrop = crops[1];
            }
            // 여기에 작물 추가
        }
        return selectedCrop;
    }
}
