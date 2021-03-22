using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolServiceTank : PoolServiceGeneric<TankController>
{
    public TankModel tankModel;
    public TankView tankPrefab;

    public TankController GetTank(TankModel tankModel, TankView tankPrefab, Vector3 vector3, Quaternion rotation)
    {
        return CreateItem();
    }
    protected override TankController CreateItem()
    {
        TankController tankController = new TankController(tankPrefab, tankModel, Vector3.zero, Quaternion.identity);
        return tankController;
    }

}
