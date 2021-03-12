using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootContainerInteract : Interactable
{
    [SerializeField] GameObject closed;
    [SerializeField] GameObject opened;
    [SerializeField] bool isOpened;

    public override void Interact(Character character)
    {
        if(isOpened == false)
        {
            isOpened = true;
            closed.SetActive(false);
            opened.SetActive(true);
        }
    }
}
