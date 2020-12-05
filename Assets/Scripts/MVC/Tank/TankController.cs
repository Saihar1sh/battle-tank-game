using UnityEngine;

public class TankController 
{
    public TankController(TankView tankPrefab, TankModel tankModel)
    {
        TankView = GameObject.Instantiate<TankView>(tankPrefab);
        TankView.SetTankDetails(tankModel);
    }
    public TankController(EnemyView enemyPrefab, TankModel tankModel)
    {
       EnemyView  = GameObject.Instantiate<EnemyView>(enemyPrefab);
    }
    public TankView TankView { get; }
    public EnemyView EnemyView { get; }


}
