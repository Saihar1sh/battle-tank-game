using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankModel 
{
    public TankModel(TankScriptableObject tank)
    {
        TankColor = tank.tankColor;
        mvtSpeed = tank.MvtSpeed;
        rotatingSpeed = tank.RotatingSpeed;
        health = tank.Health;
        reloadTime = tank.BulletReloadTime;
    }

    public Color TankColor { get; }
    public float mvtSpeed{ get; }
    public float rotatingSpeed { get; }
    public int health { get; }
    public float reloadTime { get; }
}
