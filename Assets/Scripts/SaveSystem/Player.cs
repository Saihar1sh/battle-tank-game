using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    internal int enemiesKilled;
    internal int bulletsFired;
    internal int waveNo;
    internal float currentHealth;

    private TankView tank;

    private void OnEnable()
    {
        tank = GetComponent<TankView>();

    }
    private void Update()
    {
        enemiesKilled = TankService.Instance.enemiesDestroyed;
        waveNo = TankService.Instance.waves;
        bulletsFired = BulletService.Instance.bulletsFired;
        currentHealth = tank.ReturnCurrentHealth();
    }

    public void SaveGame()
    {
        OnEnable();
        SaveSystem.SavePlayer(this);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        TankService.Instance.SetEnemiesDestroyedCount(data.enemiesKilled);
        BulletService.Instance.SetBulletsFired(data.shellsFired);
        tank.SetCurrentHealth(data.playerHealth);
        TankService.Instance.SetCurrentWave(data.waveNo);

        Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

        transform.position = position;
    }

}
