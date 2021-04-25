using UnityEngine;

public class PickupService : MonoSingletonGeneric<PickupService>
{
    //public PickupScriptableList pickupScriptableList;
    public GameObject[] pickups;

    private int meshIndex;
    private float timeCheck = 1;

    public int rampagesPicked;
    // Start is called before the first frame update
    void Start()
    {
        #region TryLater
        /*        for (int i = 0; i < pickupScriptableList.pickupScriptableObj.Length; i++)
                {
                    PickupScriptable pickup_Scriptable=  pickupScriptableList.pickupScriptableObj[i];
                    Instantiate<GameObject>(pickup_Scriptable.pickup);
                    pickup_Scriptable.pickup.SetActive(false);


                    PickupType _pickupType = pickup_Scriptable.pickupType;
                    switch (_pickupType)
                    {
                        case PickupType.None:
                            break;
                        case PickupType.Health:
                            break;
                        case PickupType.FireAmmo:
                            break;
                        case PickupType.RapidAmmo:
                            break;
                        case PickupType.Rampage:
                            break;

                    }
                }
        */
        #endregion

        for (int i = 0; i < pickups.Length; i++)
        {
            if (pickups[i].GetComponent<SpriteRenderer>() != null)
            {
                pickups[i].GetComponent<SpriteRenderer>().enabled = true;
            }
            if (pickups[i].GetComponent<MeshRenderer>() != null)
            {
                meshIndex = i;
                pickups[i].GetComponent<MeshRenderer>().enabled = true;
            }
            pickups[i].SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {

        timeCheck += 1 * Time.deltaTime;
        if (timeCheck >= 15f)
        {
            Vector3 randomSpawnPos = new Vector3(Random.Range(-38, 38), 0, Random.Range(38, -38));

            int randval = Random.Range(0, pickups.Length);
            pickups[randval].transform.position = randomSpawnPos;
            if (meshIndex == randval)
                pickups[meshIndex].GetComponent<MeshRenderer>().enabled = true;
            else
                pickups[randval].GetComponent<SpriteRenderer>().enabled = true;
            pickups[randval].SetActive(true);
            timeCheck = 0f;
        }


    }

}
