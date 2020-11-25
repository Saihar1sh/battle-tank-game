using UnityEngine;

public class TankController 
{
    public TankController(TankView tankPrefab, TankModel tankModel)
    {
        TankView = GameObject.Instantiate<TankView>(tankPrefab);
    }

    public TankView TankView { get; }
    //public void PlayerTouchInput() { public get} 


}
