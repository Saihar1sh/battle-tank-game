using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolEnemyService : PoolServiceGeneric<TankController>
{
    private TankModel tankModel;
    private EnemyView enemyPrefab;

    public TankController GetEnemy(TankModel tankModel, EnemyView enemyPrefab, Vector3 vector3, Quaternion rotation)
    {
        this.tankModel = tankModel;
        this.enemyPrefab = enemyPrefab;
        return GetItem();
    }

    protected override TankController CreateItem()
    {
        TankController tankController = new TankController(enemyPrefab, tankModel, Vector3.zero, Quaternion.identity);
        return tankController;
    }

}
