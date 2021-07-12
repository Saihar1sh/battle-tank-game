using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    internal int enemiesKilled;
    internal int shellsFired;
    internal float currentHealth;

    private TankView tank;

    private void Start()
    {
        tank = GetComponent<TankView>();
    }

    private void Update()
    {
        enemiesKilled = TankService.Instance.enemiesDestroyed;
        shellsFired = BulletService.Instance.bulletsFired;
        currentHealth = tank.ReturnCurrentHealth();
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        TankService.Instance.SetEnemiesDestroyedCount(data.enemiesKilled) ;
        BulletService.Instance.SetBulletsFired(data.shellsFired);
        tank.SetCurrentHealth(data.playerHealth);

        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

        transform.position = position;
    }

}
