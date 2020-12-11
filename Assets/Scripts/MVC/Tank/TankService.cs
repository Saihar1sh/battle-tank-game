﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankView TankView;
    public EnemyView Enemy;
    public TankScriptableObjectList tankList;

    private float randomPosX, randomPosZ;

    public List<TankView> tanks;
    public List<EnemyView> enemyTanks;
    public GameObject[] environment;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
         CreateTank(tankList.tanks[1]);
        for (int i = 0; i < 5; i++)
        {
            CreateEnemyTank(tankList.tanks[0]);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            CreateEnemyTank(tankList.tanks[0]);
            Debug.Log("Creating EnemyTank");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            CreatePlayerTank();
        }
    }

    public void CreatePlayerTank()
    {
        int i = Random.Range(1, tankList.tanks.Length);
        CreateTank(tankList.tanks[i]);
        Debug.Log("Creating Tank");
    }

    public TankController CreateTank(TankScriptableObject tankScriptableObject)
    {
        randomPosX = Random.Range(-42, 43);
        randomPosZ = Random.Range(39, -43);
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(TankView, tankModel, new Vector3(randomPosX, 0, randomPosZ), Quaternion.identity);
        return tankController;
    }
    public TankController CreateEnemyTank(TankScriptableObject tankScriptableObject)
    {
        randomPosX = Random.Range(-42, 43);
        randomPosZ = Random.Range(39, -43);
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(Enemy, tankModel, new Vector3(randomPosX, 0, randomPosZ), Quaternion.identity);
        return tankController;

    }
    public void DestroyEverything()
    {
        StartCoroutine(Destruction());
    }
    public void DestroyEnvironment()
    {
        environment = GameObject.FindObjectsOfType<GameObject>();
        StartCoroutine(DestroyEnvironmentDelay(environment));
    }
/*    public void DestroyTanks()
    {
        StartCoroutine(DestroyTanksDelay());
    }
*/    public void DestroyEnemies()
    {
        StartCoroutine(DestroyEnemyDelay());
    }

/*    private IEnumerator DestroyTanksDelay()
    {
        while(tanks.Count != 0)
        {
            Destroy(tanks[0].gameObject);
            tanks.RemoveAt(0);
            yield return new WaitForSeconds(1f);
        }
    }

*/    private IEnumerator DestroyEnemyDelay()
    {
        while (enemyTanks.Count != 0)
        {
            Destroy(enemyTanks[0].gameObject);
            enemyTanks.RemoveAt(0);
            yield return new WaitForSeconds(1f);
        }
    }
    private IEnumerator DestroyEnvironmentDelay(GameObject[] gameObjects)
    {
        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i].gameObject);
            Debug.Log(gameObjects[i]);
            yield return new WaitForSeconds(.1f);
        }
    }
    private IEnumerator Destruction()
    {
        yield return null;
        StartCoroutine(DestroyEnemyDelay());
        StartCoroutine(DestroyEnvironmentDelay(environment));
    }
}
