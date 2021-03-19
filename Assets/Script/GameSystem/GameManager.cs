using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    public GameObject player; 
    public Item_Container itemContainer;
    public Item_DragDrop itemDragDrop;
}
