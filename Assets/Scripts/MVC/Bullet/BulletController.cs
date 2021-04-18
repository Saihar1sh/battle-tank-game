using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody bulletRB;
    // public float maxDamage, explosionForce, explosionRadius;
    public float maxLifetime = 2f, bulletSpeed = 20f, damage = 10f;

    private MeshRenderer meshRenderer;

    //private Particles ParticlesInstance;
    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody>();
        meshRenderer = gameObject.GetComponent<MeshRenderer>();
        Destroy(gameObject, maxLifetime);
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        bulletRB.velocity = Vector3.zero;
        meshRenderer.enabled = true;
        bulletRB.AddForce(transform.forward * bulletSpeed);
    }
    private void Update()
    {

    }

    //To affect surroundings of bullet
    private void OnTriggerEnter(Collider other)
    {
        #region damage radius develop later
        /*        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);
                for (int i = 0; i < colliders.Length; i++)
                {
                    Rigidbody targetRB = colliders[i].GetComponent<Rigidbody>();
                    if (!targetRB)
                    {
                        Debug.LogWarning("No Rigidbody attached to : " + colliders[i].name);
                        continue;
                    }
                    targetRB.AddExplosionForce(explosionForce, transform.position, explosionRadius);

                }
        */
        #endregion
        Particles.Instance.CommenceShellExplosion(transform);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Particles.Instance.CommenceShellExplosion(transform);
        bool fireAmmo = BulletService.Instance.fireAmmoBool;
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            Particles.Instance.CommenceTankExplosion(collision.transform);
            EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
            enemy.ModifyHealth(-damage);
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            Particles.Instance.CommenceTankExplosion(collision.transform);
            TankView player = collision.gameObject.GetComponent<TankView>();
            player.ModifyHealth(-damage);
        }
        if (collision.gameObject.tag.Equals("Environment"))
        {
            if (fireAmmo)
                Destroy(collision.gameObject);

        }
        PoolService.Destroy(gameObject);
        //meshRenderer.enabled = false;
        gameObject.SetActive(false);
    }
    #region damage radius develop later

    /*    private float CalculateDamage(Vector3 targetPos)
        {
            Vector3 explosionToTarget = targetPos - transform.position;
            float explosionDist = explosionToTarget.magnitude;
            float relativeDist = (explosionRadius - explosionDist) / explosionRadius;
            float damage = relativeDist * maxDamage;
            damage = Mathf.Max(0f, damage);
            return damage;
        }
    */
    #endregion
}
