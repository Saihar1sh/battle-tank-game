using UnityEngine;

[CreateAssetMenu(fileName = "PickupScriptableObject", menuName = "ScriptableObjects/Pickup")]
public class PickupScriptable : ScriptableObject 
{ 
    public PickupType pickupType;
    public GameObject pickup;
}


[CreateAssetMenu(fileName = "PickupScriptableObjectList", menuName = "ScriptableObjects/PickupsList")]
public class PickupScriptableList : ScriptableObject
{
    public PickupScriptable[] pickupScriptableObj;

}

public enum PickupType
{
    None,
    Health,
    RapidAmmo,
    FireAmmo,
    Rampage
}