using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankService : MonoSingletonGeneric<TankService>
{
    public TankView TankView;
    public EnemyView Enemy;
    public TankScriptableObjectList tankList;
    public GameObject tankExplosion;

    private ParticleSystem explosionParticles;
    private AudioSource tankExplosionAudio;
    private float randomPosX, randomPosZ;
    protected override void Awake()
    {
        base.Awake();
        explosionParticles = Instantiate(tankExplosion).GetComponent<ParticleSystem>();
        explosionParticles.gameObject.transform.localScale *= 5;
        tankExplosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.gameObject.SetActive(false);
    }
    void Start()
    {
         CreateTank(tankList.tanks[1]);
        for (int i = 0; i < 5; i++)
        {
            CreateEnemyTank(tankList.tanks[0]);
        }
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
            int i = Random.Range(1, tankList.tanks.Length);
            CreateTank(tankList.tanks[i]);
            Debug.Log("Creating Tank");
        }
    }
    public TankController CreateTank(TankScriptableObject tankScriptableObject)
    {
        randomPosX = Random.Range(-42, 43);
        randomPosZ = Random.Range(39, -43);
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(TankView, tankModel, new Vector3(randomPosX, 0, randomPosZ), Quaternion.identity);
        return tankController;
    }
    public TankController CreateEnemyTank(TankScriptableObject tankScriptableObject)
    {
        randomPosX = Random.Range(-42, 43);
        randomPosZ = Random.Range(39, -43);
        TankModel tankModel = new TankModel(tankScriptableObject);
        TankController tankController = new TankController(Enemy, tankModel, new Vector3(randomPosX, 0, randomPosZ), Quaternion.identity);
        return tankController;

    }

    public void CommenceExplosion(Vector3 _pos)
    {
        Debug.Log("Explosion........");
        explosionParticles.transform.position = _pos;
        explosionParticles.gameObject.SetActive(true);
        explosionParticles.Play();
        tankExplosionAudio.Play();
    }

}
