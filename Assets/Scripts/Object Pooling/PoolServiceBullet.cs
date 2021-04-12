using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolServiceBullet : PoolServiceGeneric<BulletController>
{
    public BulletScriptableObject bulletScriptableObject;
    public BulletController bullet;

    public BulletController GetTank(BulletScriptableObject bulletScriptableObject, BulletController bullet, Vector3 vector3, Quaternion rotation)
    {
        return CreateItem();
    }

    protected override BulletController CreateItem()
    {
        Transform transfor = transform;
        transfor.position = Vector3.zero;
        transfor.rotation = Quaternion.identity;
        BulletController bulletController = BulletService.Instance.InstantiateBullet(transfor);
        return bulletController;
    }
}
