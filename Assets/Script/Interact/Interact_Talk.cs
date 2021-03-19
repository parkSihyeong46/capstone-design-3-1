using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact_Talk : Interactable
{
    public override void Interact(Character character)
    {
        Debug.Log("Talking");
    }
}
