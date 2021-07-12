using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int incrementHealthAmt;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<TankView>() != null)
            {
                TankView tankView = other.gameObject.GetComponent<TankView>();
                tankView.ModifyHealth(+incrementHealthAmt);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;

            }
        }

    }
}

