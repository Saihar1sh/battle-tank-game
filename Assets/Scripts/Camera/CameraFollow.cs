using UnityEngine;
using Cinemachine;

public class CameraFollow : MonoBehaviour
{
    private TankView player;

    //cameras-----------------------------------------
    [SerializeField]
    private Camera MainCamera;
    [SerializeField]
    private CinemachineVirtualCamera TPPCam;

    private void Update()
    {
    }
    private void FixedUpdate()
    {
        if(player == null)
        {
           player = FindObjectOfType<TankView>();

        }
        else
        {
            MainCamera.transform.LookAt(player.transform);
            TPPCam.m_Follow = player.transform;
            TPPCam.m_LookAt = player.transform;
        }
    }
}
