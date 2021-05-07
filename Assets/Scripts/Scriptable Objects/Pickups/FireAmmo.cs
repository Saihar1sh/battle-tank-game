using UnityEngine;

public class FireAmmo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<TankView>() != null)
            {
                BulletService.Instance.fireAmmoBool = true;
                UIManager.Instance.FireAmmoMode();
                gameObject.SetActive(false);
            }
        }

    }
}
