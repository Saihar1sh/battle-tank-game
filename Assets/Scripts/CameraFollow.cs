using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void FixedUpdate()
    {
        transform.LookAt(player);
    }
}
