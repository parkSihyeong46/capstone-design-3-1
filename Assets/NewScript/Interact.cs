using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public enum UseTool
    {
        Hand = -1,
        Axe = 0,
        Pick = 1,
        Hoe = 2,
        WateringCans = 3,
    }

    public UseTool useTool = UseTool.Hand;
    
    public virtual void DoInteract(Character character)
    {
        //각각 오버라이드로 사용
    }
}
