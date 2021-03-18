using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<TankView>() != null)
            {
/*                TankView tankView = other.gameObject.GetComponent<TankView>();
                tankView.fireBulletBool = true;
*/              BulletService.Instance.fireAmmoBool = true;
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

    }
}
