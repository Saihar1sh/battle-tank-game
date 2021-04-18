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
    public void InstantiateBullet(Transform t)
    {
        Instantiate(BulletPrefab.gameObject, t.position, t.rotation);
        bulletsFired++;
    }
    public void InstantiateFireBullet(Transform t)
    {
        Instantiate(FireBulletPrefab.gameObject, t.position, t.rotation);
        bulletsFired++;
    }

    public void SetBulletsFired(int i)
    {
        bulletsFired = i;
    }

}
