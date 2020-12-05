using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankView TankView;
    public EnemyView Enemy;
    public TankScriptableObjectList tankList;

    void Start()
    {
        CreateTank(tankList.tanks[1]);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CreateEnemyTank(tankList.tanks[0]);
            Debug.Log("Creating EnemyTank");
        }
    }
    public TankController CreateTank(TankScriptableObject tankScriptableObject)
    {
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(TankView, tankModel);
        return tankController;
    }
    public TankController CreateEnemyTank(TankScriptableObject tankScriptableObject)
    {
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(Enemy, tankModel);
        return tankController;

    }

}
