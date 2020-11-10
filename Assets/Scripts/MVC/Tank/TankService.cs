using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public GameObject tank;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void GetTank()
    {
        Instantiate(tank);
    }
}
