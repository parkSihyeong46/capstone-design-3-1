using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInit : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("캐릭터 인벤 정보를 통해 아이템 배열을 받아옴");
        Debug.Log("받아온 아이템 배열을 통해 아이템 없는 자리의 아이템 이미지 disabled");
        Debug.Log("아이템 있는 자리는 enabled, 해당 아이템으로 이미지 설정\n");

        Debug.Log("캐릭터 장비 정보를 받아옴");
        Debug.Log("장비 장착 중이면 해당 부분 이미지 변경, 장착 중이지 않으면, 지정해둔 기본 이미지 변경");
    }

    private void OnDisable()
    {
        
    }
}
