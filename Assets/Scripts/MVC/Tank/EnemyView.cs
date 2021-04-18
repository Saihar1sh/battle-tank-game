using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour, IDamagable
{
    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, maxHealth = 200;

    private float currentHealth;
    private bool canShoot = true;
    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;

    private HealthBar HealthBar;
    private TankModel redTankModel;
    [SerializeField]
    private Transform tankTurret, tankShootPos;

    private NavMeshAgent meshAgent;

    public int Id;

    private void Awake()
    {
        //referencing
        HealthBar = GetComponentInChildren<HealthBar>();
        meshAgent = GetComponent<NavMeshAgent>();

    }
    private void OnEnable()
    {
        AddDetails();
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);

    }
    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        Debug.Log("Enemy Id: " + Id, gameObject);
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
            DestroyEnemyTank();
    }

    public void MoveTurret(Transform player)
    {
        tankTurret.transform.LookAt(player);
    }
    public void Shoot()
    {
        //Debug.Log("shoot");
        BulletService.Instance.InstantiateBullet(tankShootPos);
    }

    public void AddDetails()
    {
        redTankModel = new TankModel(TankService.Instance.tankList.tanks[0]);
        SetEnemyDetails(redTankModel);
        TankService.Instance.enemyTanks.Add(this);

    }
    public void SetEnemyDetails(TankModel model)
    {
        mvtSpeed = model.mvtSpeed;
        rotatingSpeed = model.rotatingSpeed;
        maxHealth = model.health;
        //Color color = model.TankColor;
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color.red;
    }

    public void ModifyHealth(float amount)
    {
        currentHealth += amount;
        HealthBar.SetHealth(currentHealth);
    }
    public void DestroyEnemyTank()
    {
        Particles.Instance.CommenceTankExplosion(transform);
        gameObject.SetActive(false);
        TankService.Instance.SpawnBustedTank(transform);
        TankService.Instance.IncreamentEnemyDeathCounter();
        TankService.Instance.enemyTanks.Remove(this);
        ServiceEvents.Instance.OnEnemyDeathInvoke();
        PoolService.Destroy(gameObject);
    }
    public void ShootDelay(float secs)
    {
        if (canShoot)
            StartCoroutine(ShootBulletDelay(secs));
    }
    IEnumerator ShootBulletDelay(float secs)
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(secs);
        canShoot = true;
    }

    private void OnDestroy()
    {
    }
}

