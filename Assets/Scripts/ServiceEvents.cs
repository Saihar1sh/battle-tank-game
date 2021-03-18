using System;

public class ServiceEvents : MonoSingletonGeneric<ServiceEvents>
{
    public event Action OnPlayerDeath;
    public event Action OnEnemyDeath;
    public event Action OnEnemiesDestroyed;
    public event Action OnPlayersDestroyed;
    public event Action OnBulletsFired;

    public int enemiesDied { get; private set; }
    public int bulletsUsed { get; private set; }
    public int playersDied{ get; private set; }
    public int  bulletsFiredAchievement = 50, enemiesDeadAchievement = 10, playersDeadAchievement = 5;

    private void Update()
    {
        enemiesDied = TankService.Instance.enemiesDestroyed;
        playersDied = TankService.Instance.playersDestroyed;
        bulletsUsed = BulletService.Instance.bulletsFired;
        if (enemiesDied >= enemiesDeadAchievement)
        {
            OnEnemiesDestroyed?.Invoke();
            enemiesDeadAchievement += 20;
        }
        if(playersDied > playersDeadAchievement)
        {
            OnPlayersDestroyed?.Invoke();
            playersDeadAchievement += 10;
        }
        if(bulletsUsed > bulletsFiredAchievement)
        {
            OnBulletsFired?.Invoke();
            bulletsFiredAchievement += 50;
        }
    }
    public void OnEnemyDeathInvoke()
    {
        OnEnemyDeath?.Invoke();
    }
    public void OnPlayerDeathInVoke()
    {
        OnPlayerDeath?.Invoke();
    }
}
