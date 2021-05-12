using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObjects/Bullet")]
public class BulletScriptableObject : ScriptableObject
{
    public BulletType bulletType;
    public float mvtSpeed;

}

[CreateAssetMenu(fileName = "BulletScriptableObjectList", menuName = "ScriptableObjects/BulletList")]
public class BulletScriptableObjectList : ScriptableObject
{
    public BulletScriptableObject[] bullets;
}

