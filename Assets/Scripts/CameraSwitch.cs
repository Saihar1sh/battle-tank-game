using UnityEngine;
using UnityEngine.UI;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField]
    private GameObject cam , cam3rdPerson;
    [SerializeField]
    private Button switchCamBtn;

    private int count = 0;

    private void Awake()
    {
        switchCamBtn.onClick.AddListener(SwitchCam);    
    }
    private void Start()
    {
        cam.SetActive(true);
        cam3rdPerson.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && count >= 0)
        {
            SwitchCam();
        }
    }
    private void SwitchCam()
    {
         count++;
         Camera(count);

    }
    private void Camera(int _count)
    {
        switch (_count)
        {
            case 1:
                cam.SetActive(false);
                cam3rdPerson.SetActive(true);
                break;
            case 2:
                cam3rdPerson.SetActive(false);
                cam.SetActive(true);
                count = 0;
                break;

        }
    }
}
