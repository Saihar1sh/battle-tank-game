using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolServiceTank : PoolServiceGeneric<TankController>
{
    private TankModel tankModel;
    private TankView tankPrefab;
    private EnemyView enemyPrefab;

    private bool enemyNeeded = false, playerNeeded = false;

    public TankController GetTank(TankModel tankModel, TankView tankPrefab, Vector3 vector3, Quaternion rotation)
    {
        playerNeeded = true;
        this.tankModel = tankModel;
        this.tankPrefab = tankPrefab;
        return GetItem();
    }
    public TankController GetEnemy(TankModel tankModel, EnemyView enemyPrefab, Vector3 vector3, Quaternion rotation)
    {
        enemyNeeded = true;
        this.tankModel = tankModel;
        this.enemyPrefab = enemyPrefab;
        return GetItem();

    }
    protected override TankController CreateItem()
    {
        TankController tankController = new TankController(tankPrefab, tankModel, Vector3.zero, Quaternion.identity);
        return tankController;
    }



}
