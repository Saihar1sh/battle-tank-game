using System;
using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObjects/Tank")]
public class TankScriptableObject : ScriptableObject
{
    //public TankColor tankColor;
    public Color tankColor;
    public string TankName;
    public float MvtSpeed, RotatingSpeed, BulletReloadTime;
    public int Health = 100;
}


