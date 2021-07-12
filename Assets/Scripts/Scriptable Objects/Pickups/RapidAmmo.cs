using System.Collections;
using UnityEngine;

public class RapidAmmo : MonoBehaviour
{
    private bool rapidAmmoBool = true;

    public float rapidAmmoTime = 10f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<TankView>() != null)
            {
                TankView tankView = other.gameObject.GetComponent<TankView>();
                if(rapidAmmoBool)
                    StartCoroutine(RapidAmmoTime(tankView, rapidAmmoTime));
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }
    IEnumerator RapidAmmoTime(TankView tankView, float timePeriod)
    {
        rapidAmmoBool = false;
        float delay = tankView.shootDelay;
        tankView.shootDelay = 0;
        yield return new WaitForSeconds(timePeriod);
        tankView.shootDelay = delay;
        rapidAmmoBool = true;
    }
}
