using UnityEngine;

public class Particles : MonoSingletonGeneric<Particles>
{
    [SerializeField]
    private ParticleSystem tankExplosion;
    //[SerializeField]
    //private ParticleSystem explosionSmoke;
    [SerializeField]
    private ParticleSystem shellExplosion;

    public void CommenceTankExplosion(Transform t)
    {
        ParticleSystem explosionParticles = Instantiate(tankExplosion, t.position, t.rotation);
        //ParticleSystem smokeParticles = Instantiate(explosionSmoke, t.position, t.rotation);
        explosionParticles.Play();
        //smokeParticles.Play();
        Destroy(explosionParticles.gameObject, 1f);
    }

    public void CommenceShellExplosion(Transform tr)
    {
        ParticleSystem ps = Instantiate(shellExplosion, tr.position, tr.rotation);
        ps.Play();
        Destroy(ps.gameObject, 1f);
    }
}