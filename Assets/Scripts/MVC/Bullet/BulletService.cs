using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    [SerializeField]
    private BulletController BulletPrefab, FireBulletPrefab;

    public bool fireAmmoBool;

    public int bulletsFired { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    public void InstantiateBullet(Transform transform)
    {
        Instantiate(BulletPrefab, transform.position, transform.rotation);
        bulletsFired++;
    }
    public void InstantiateFireBullet(Transform transform)
    {
        Instantiate(FireBulletPrefab, transform.position, transform.rotation);
        bulletsFired++;
    }

    public void SetBulletsFired(int i)
    {
        bulletsFired = i;
    }

}
