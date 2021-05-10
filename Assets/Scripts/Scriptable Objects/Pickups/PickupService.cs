using UnityEngine;

public class PickupService : MonoSingletonGeneric<PickupService>
{
    //public PickupScriptableList pickupScriptableList;
    public GameObject[] pickups;

    private float timeCheck = 1;

    public int rampagesPicked;
    // Start is called before the first frame update
    void Start()
    {
        DisablePickups();
    }

    private void DisablePickups()
    {
        for (int i = 0; i < pickups.Length; i++)
        {
            pickups[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.Instance.playerGodMode)
        {
            DisablePickups();
            return;
        }
        else
        {
            timeCheck += 1 * Time.deltaTime;
            if (timeCheck >= 15f)
            {
                Vector3 randomSpawnPos = new Vector3(Random.Range(-38, 38), 0, Random.Range(38, -38));

                int randval = Random.Range(0, pickups.Length);
                pickups[randval].transform.position = randomSpawnPos;
                pickups[randval].SetActive(true);
                timeCheck = 0f;
            }

        }
    }

}
