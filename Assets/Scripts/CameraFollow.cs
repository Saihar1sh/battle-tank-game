using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private float smoothSpeed = .125f;
    [SerializeField]
    private Vector3 offset;
    private Vector3 vel = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 desiredPos = transform.position + offset;
        Vector3 smoothedPos = Vector3.SmoothDamp(transform.position, desiredPos, ref vel , smoothSpeed);
        transform.LookAt(player);
    }
}
