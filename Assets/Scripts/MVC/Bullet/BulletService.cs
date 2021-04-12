using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    [SerializeField]
    private BulletController BulletPrefab, FireBulletPrefab;


    public bool fireAmmoBool;

    private Transform transForm;
    public int bulletsFired { get; private set; }

    protected override void Awake()
    {
        base.Awake();
    }
    public void GetBullet(Transform t)
    {
        transForm = t;
        PoolServiceBullet.Instance.GetItem();
    }
    public BulletController InstantiateBullet(Transform t)
    {
        BulletController bulletController = Instantiate(BulletPrefab, transForm.position, transForm.rotation);
        bulletsFired++;
        return bulletController;

    }
    public BulletController InstantiateFireBullet(Transform transform)
    {
        BulletController bulletController = Instantiate(FireBulletPrefab, transform.position, transform.rotation);
        bulletsFired++;
        return bulletController;
    }

    public void SetBulletsFired(int i)
    {
        bulletsFired = i;
    }

}
