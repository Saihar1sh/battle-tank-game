using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask tankMask;
    private Rigidbody bulletRB;
    public float maxDamage, explosionForce, explosionRadius, maxLifetime = 2f, bulletSpeed= 20f;
    private void Awake()
    {
        bulletRB = GetComponent<Rigidbody>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Vector3 move = transform.position + transform.forward * bulletSpeed * Time.fixedDeltaTime;
        bulletRB.velocity = move;
        Destroy(gameObject, maxLifetime);
    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyView>() != null)
        {
            Particles.Instance.CommenceTankExplosion(other.transform);
            EnemyView enemy = other.gameObject.GetComponent<EnemyView>();
            enemy.DestroyEnemyTank();
            /*                float health = enemy.health;

                            float damage = CalculateDamage(targetRB.position);
                            enemy.TakeDamage(damage);
            */

        }

        /* Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, tankMask);
         for (int i = 0; i < colliders.Length; i++)
         {
             Rigidbody targetRB = colliders[i].GetComponent<Rigidbody>();
             if (!targetRB)
             {
                 Debug.LogWarning("No Rigidbody attached to : " + colliders[i].name);
                 continue;
             }
             targetRB.AddExplosionForce(explosionForce, transform.position, explosionRadius);

         }*/
        Particles.Instance.CommenceShellExplosion(transform);
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
