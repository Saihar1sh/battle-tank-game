using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public LayerMask tankMask;
    private Rigidbody bulletRB;
    public float maxDamage, explosionForce, explosionRadius, maxLifetime = 2f, bulletSpeed= 20f;
    public float damage = 10f;

    //private Particles ParticlesInstance;
    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //ParticlesInstance = (Particles) Particles.GetInstance();
        bulletRB.AddForce(transform.forward * bulletSpeed);
        Destroy(gameObject, maxLifetime);
    }
    private void Update()
    {
        
    }
    /* To affect surroundings of bullet
     private void OnTriggerEnter(Collider other)
        {
           
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);
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
            Particles.Instance.CommenceShellExplosion(transform);
        }
    */

    private void OnCollisionEnter(Collision collision)
    {
        Particles.Instance.CommenceShellExplosion(transform);
            if(collision.gameObject.tag.Equals("Enemy"))
            {
                Particles.Instance.CommenceTankExplosion(collision.transform);
                EnemyView enemy = collision.gameObject.GetComponent<EnemyView>();
                enemy.TakeDamage(damage);
            }
            if (collision.gameObject.tag.Equals("Player"))
            {
                Particles.Instance.CommenceTankExplosion(collision.transform);
                TankView player = collision.gameObject.GetComponent<TankView>();
                player.ModifyHealth(damage);
            }
            if(collision.gameObject.tag.Equals("Environment"))
            {
                Destroy(gameObject);
            }
    }
    private float CalculateDamage(Vector3 targetPos)
    {
        Vector3 explosionToTarget = targetPos - transform.position;
        float explosionDist = explosionToTarget.magnitude;
        float relativeDist = (explosionRadius - explosionDist) / explosionRadius;
        float damage = relativeDist * maxDamage;
        damage = Mathf.Max(0f, damage);
        return damage;
    }
}
