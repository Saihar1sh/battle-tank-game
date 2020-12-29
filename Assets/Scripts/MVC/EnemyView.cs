using System;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, maxHealth;

    private float currentHealth;
    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;

    private HealthBar HealthBar;

    private void Awake()
    {
        HealthBar = GetComponentInChildren<HealthBar>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
            DestroyEnemyTank();
    }

    private void EnemyMovement()
    {

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

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        HealthBar.SetHealth(currentHealth);
    }
    public void DestroyEnemyTank()
    {
        Particles.Instance.CommenceTankExplosion(transform);
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
    }
    private void OnDestroy()
    {
        TankService.Instance.enemyTanks.Remove(this);
    }
}

