using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Transform player;
    private void Start()
    {

    }
    private void LateUpdate()
    {
        if (player == null)
        {
            player = FindObjectOfType<TankView>().transform;

        }
        else
        {
            Vector3 newPos = player.position;
            newPos.y = transform.position.y;
            transform.position = newPos;

            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
    }
}
