using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, health;

    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyMovement()
    {

    }
    public void SetEnemyDetails(TankModel model)
    {
        mvtSpeed = model.mvtSpeed;
        rotatingSpeed = model.rotatingSpeed;
        health = model.health;
        TankColor color = model.TankColor;
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Bullet>() != null)
        {
            DestroyEnemyTank();
        }
    }
    public void TakeDamage(float amount)
    {

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

