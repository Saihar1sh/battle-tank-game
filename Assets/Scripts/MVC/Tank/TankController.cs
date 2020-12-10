using UnityEngine;

public class TankController 
{
    public TankController(TankView tankPrefab, TankModel tankModel, Vector3 _pos, Quaternion _rotation)
    {
        TankView = GameObject.Instantiate<TankView>(tankPrefab, _pos, _rotation);
        TankView.SetTankDetails(tankModel);
    }
    public TankController(EnemyView enemyPrefab, TankModel tankModel, Vector3 _pos, Quaternion _rotation)
    {
        EnemyView  = GameObject.Instantiate<EnemyView>(enemyPrefab, _pos, _rotation);
        EnemyView.SetEnemyDetails(tankModel);
    }


    public TankView TankView { get; }
    public EnemyView EnemyView { get; }


}
