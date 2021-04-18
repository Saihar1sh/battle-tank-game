using System.Collections;
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
        GameObject explosionP = PoolService.Instantiate(tankExplosion.gameObject, t.position, t.rotation);
        ParticleSystem explosionParticles = explosionP.GetComponent<ParticleSystem>();
        //ParticleSystem smokeParticles = Instantiate(explosionSmoke, t.position, t.rotation);
        explosionParticles.Play();
        //smokeParticles.Play();
        StartCoroutine(GameobjectReturnToPoolDelay(explosionP, 1f));
    }

    public void CommenceShellExplosion(Transform tr)
    {
        GameObject shellExpl = PoolService.Instantiate(shellExplosion.gameObject, tr.position, tr.rotation);
        ParticleSystem ps = shellExpl.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps.gameObject, 1f);
        StartCoroutine(GameobjectReturnToPoolDelay(shellExpl, 1f));
    }
    IEnumerator GameobjectReturnToPoolDelay(GameObject _gameObject, float secs)
    {
        yield return new WaitForSeconds(secs);
        if (_gameObject != null)
        {
            _gameObject.SetActive(false);
            PoolService.Destroy(_gameObject);

        }
    }

}