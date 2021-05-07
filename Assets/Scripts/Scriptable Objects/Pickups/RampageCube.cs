using System.Collections;
using UnityEngine;

public class RampageCube : MonoBehaviour
{
    private bool rapidAmmoBool = true;

    public float rampageTime = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<TankView>() != null)
            {
                UIManager.Instance.RampageMode();
                TankView tankView = other.gameObject.GetComponent<TankView>();
                if (rapidAmmoBool)
                    StartCoroutine(RapidFireAmmoTime(tankView, rampageTime));
                gameObject.SetActive(false);

            }
        }

    }
    IEnumerator RapidFireAmmoTime(TankView tankView, float timePeriod)
    {
        rapidAmmoBool = false;
        float delay = tankView.shootDelay;
        tankView.shootDelay = 0;
        BulletService.Instance.fireAmmoBool = true;
        yield return new WaitForSeconds(timePeriod);
        BulletService.Instance.fireAmmoBool = false;
        tankView.shootDelay = delay;
        rapidAmmoBool = true;
    }
}
