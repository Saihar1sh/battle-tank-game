using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolServiceTank : PoolServiceGeneric<TankController>
{
    private TankModel tankModel;
    private TankView tankPrefab;


    public TankController GetTank(TankModel tankModel, TankView tankPrefab, Vector3 vector3, Quaternion rotation)
    {
        this.tankModel = tankModel;
        this.tankPrefab = tankPrefab;
        return GetItem();
    }
    protected override TankController CreateItem()
    {
        TankController tankController = new TankController(tankPrefab, tankModel, Vector3.zero, Quaternion.identity);
        return tankController;
    }



}
