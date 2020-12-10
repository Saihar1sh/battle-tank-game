using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask tankMask;
    public ParticleSystem shellExplosion;
    public AudioSource shellExplosionAudio;
    private Rigidbody bulletRB;
    public float maxDamage, explosionForce, explosionRadius, maxLifetime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        bulletRB.velocity = bulletRB.velocity * 2;
        Destroy(gameObject, maxLifetime);
    }
    private void Update()
    {
        
    }
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

            if (other.gameObject.GetComponent<EnemyView>() != null)
            {
                TankService.Instance.CommenceExplosion(other.transform.position);
                EnemyView enemy =  other.gameObject.GetComponent<EnemyView>();
                enemy.DestroyEnemyTank();
/*                float health = enemy.health;

                float damage = CalculateDamage(targetRB.position);
                enemy.TakeDamage(damage);
*/
                
            }
        }
        shellExplosion.transform.parent = null;
        shellExplosion.Play();
        shellExplosionAudio.Play();

        Destroy(shellExplosion.gameObject, 1f);
        Destroy(gameObject);
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
