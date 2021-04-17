﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Crop : Interact
{
    Crop_Grow crop; //작물이 가지고있는 스크립트
    bool checkCrop; //성장했는지 확인

    private void Start()
    {
        crop = GetComponent<Crop_Grow>();
    }

    private void FixedUpdate()
    {
        checkCrop = crop.isFullyGrown;
    }

    public override void DoInteract(Character character)
    {
        Debug.Log(crop);

        if (checkCrop) //다 자란 경우
        {
            //작물 획득. 아이템 획득 로직 확인 후 추가하기
        }
        Destroy(gameObject);    //다 자랐든 안 자랐든 Destroy
    }
}