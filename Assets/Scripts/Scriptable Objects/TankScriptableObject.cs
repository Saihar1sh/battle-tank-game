using System;
using UnityEngine;

[CreateAssetMenu(fileName ="TankScriptableObject", menuName ="ScriptableObjects/Tank")]
public class TankScriptableObject : ScriptableObject
{
    public TankColor tankColor;
    public string TankName;
    public float MvtSpeed, RotatingSpeed , Health;
    
}

[CreateAssetMenu(fileName = "TankScriptableObjectList", menuName = "ScriptableObjects/TankList")]
public class TankScriptableObjectList : ScriptableObject
{
    public TankScriptableObject[] tanks; 
}

