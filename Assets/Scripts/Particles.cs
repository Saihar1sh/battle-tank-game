using UnityEngine;

public class Particles : MonoSingletonGeneric<Particles>
{
    [SerializeField]
    private ParticleSystem tankExplosion;
    [SerializeField]
    private ParticleSystem shellExplosion;

    public void CommenceTankExplosion(Transform t)
    {
        ParticleSystem particles = Instantiate(tankExplosion, t.position, t.rotation);
        particles.Play();
        Destroy(particles.gameObject, 1f);
    }

    public void CommenceShellExplosion(Transform tr)
    {
        ParticleSystem ps = Instantiate(shellExplosion, tr.position, tr.rotation);
        ps.Play();
        Destroy(ps.gameObject, 1f);
    }
}