﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankView TankView;
    public EnemyView Enemy;
    public GameObject BustedTankPrefab;
    public TankScriptableObjectList tankList;

    private Vector3 randomSpawnPos;

    public List<TankView> tanks;
    public List<EnemyView> enemyTanks;
    public GameObject[] environment;

    private int playerId, EnemyId;
    private int waves = 1, enemyTanksIncreament = 5;

   // [HideInInspector]
    public bool waveStarted = false;

    public int enemiesDestroyed { get; private set; } = 0;
    public int playersDestroyed { get; private set; } = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    void Start()
    {
        /*        //int rand = Random.Range(1 , tankList.tanks.Length);

                //tanks[0].SetTankDetails(new TankModel(tankList.tanks[rand]));

                for (int i = 0; i < enemyTanks.Count; i++)
                {
                    //TankModel redTank = new TankModel(tankList.tanks[0]);
                }

                 CreateTank(tankList.tanks[rand]);
                for (int i = 0; i < 5; i++)
                {
                    CreateEnemyTank(tankList.tanks[0]);
                }
        */
        environment = GameObject.FindGameObjectsWithTag("Environment");

        ServiceEvents.Instance.OnPlayerDeath += PlayerDeathsCounterIncreament;
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
        EnemyWaves();
        Debug.Log("Enemies Destroyed : " + enemiesDestroyed);
    }

    public void CreatePlayerTank()
    {
        int i = Random.Range(1, tankList.tanks.Length);
        CreateTank(tankList.tanks[i]);
        Debug.Log("Creating Tank");

    }

    public TankController CreateTank(TankScriptableObject tankScriptableObject)
    {
        randomSpawnPos = new Vector3(Random.Range(-42, 43), 0, Random.Range(39, -43));
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(TankView, tankModel, randomSpawnPos, Quaternion.identity);
        return tankController;
    }
    public TankController CreateEnemyTank(TankScriptableObject tankScriptableObject)
    {
        randomSpawnPos = new Vector3(Random.Range(-42, 43), 0, Random.Range(39, -43));
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(Enemy, tankModel,randomSpawnPos, Quaternion.identity);
        return tankController;

    }
    public void SpawnBustedTank(Transform tankPos)
    {
        Vector3 pos = new Vector3(tankPos.position.x, 0, tankPos.position.z);
        GameObject bustedTank = Instantiate(BustedTankPrefab, pos, tankPos.rotation);
        Destroy(bustedTank, 2f);
    }
    #region DestroyEverthing
    /*    public void DestroyEverything()
        {
            StartCoroutine(Destruction());
        }
        public void DestroyEnvironment()
        {
            StartCoroutine(DestroyEnvironmentDelay());
        }
        public void DestroyTanks()
        {
            StartCoroutine(DestroyTanksDelay());
        }

        private IEnumerator DestroyTanksDelay()
        {
            while(tanks.Count != 0)
            {
                Destroy(tanks[0].gameObject);
                tanks.RemoveAt(0);
                yield return new WaitForSeconds(1f);
            }
    private IEnumerator DestroyEnvironmentDelay()
    {
        for (int i = 0; i < environment.Length; i++)
        {
            Destroy(environment[i].gameObject);
            Debug.Log(environment[i]);
            yield return new WaitForSeconds(.1f);
        }
    }
    */
    #endregion
    public void DestroyEnemies()
    {
        StartCoroutine(DestroyEnemyDelay());
    }

    private IEnumerator DestroyEnemyDelay()
    {
        while (enemyTanks.Count != 0)
        {
            Destroy(enemyTanks[0].gameObject);
            enemyTanks.RemoveAt(0);
            yield return new WaitForSeconds(.5f);
        }
        //StartCoroutine(DestroyEnvironmentDelay());

    }
    public void IncreamentEnemyDeathCounter()
    {
        enemiesDestroyed++;
    }
    public void SetEnemiesDestroyedCount(int h)
    {
        enemiesDestroyed = h;

    }
    public void EnemyWaves()
    {
        if(enemyTanks.Count == 0 && waveStarted == false)
        {
            StartCoroutine(WaveDelay(5));
        }
    }

    private void WaveStart()
    {
        for (int i = 0; i < enemyTanksIncreament; i++)
        {
            TankScriptableObject enemyTanker = tankList.tanks[0];
            CreateEnemyTank(enemyTanker);
        }
        waves++;
        enemyTanksIncreament += 3;
        Debug.Log("Wave :> " + waves);
    }

    IEnumerator WaveDelay(float _secs)
    {
        waveStarted = true;
        yield return new WaitForSeconds(_secs);
        WaveStart();
        waveStarted = false;

    }
    private void PlayerDeathsCounterIncreament()
    {
        playersDestroyed++;
    }
    public int GetCurrentWave()
    {
        return waves;
    }
    /*    private void OnDestroy()
        {
            Debug.Log("Commencing Clean Slate Protocol.................");
            DestroyEnemies()
        }
    */
}
