using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interact
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject opened;
    [SerializeField] bool isOpened;

    public override void DoInteract(Character character)
    {
        if(isOpened == false)
        {
            isOpened = true;
            closed.SetActive(false);
            opened.SetActive(true);
        }
    }
}
