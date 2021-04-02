using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Farming : MonoBehaviour
{
    [SerializeField] Tilemap_Farm tilemap_Farm;

    public void Plow()
    {
        tilemap_Farm.PlowGround();
    }

    public void Seed()
    {
        tilemap_Farm.SeedGround();
    }
}
