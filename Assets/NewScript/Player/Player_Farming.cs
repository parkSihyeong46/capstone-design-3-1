﻿using System.Collections;
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

    public void Watering()
    {
        tilemap_Farm.WateringGround();
    }

    public void Harvesting()
    {
        tilemap_Farm.CropHarvest();
    }
}