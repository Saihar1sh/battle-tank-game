using UnityEngine;

public class TankView : MonoBehaviour
{
    //movement values--------------------------
    public float mvtSpeed, rotatingSpeed;

    //private float touchHorizontal, touchVertical, keyBoardHorizontal, keyBoardVertical, accelaration;
    private Vector3 rotation;
    private bool touchInput = true, KeyboardInput = true;

    //references-------------------------------
    private Joystick joystick;
    private Rigidbody tankRb;

       private void Awake()
       {
           tankRb = GetComponent<Rigidbody>();
           joystick = FindObjectOfType<FixedJoystick>();
       }
        void Start()
        {

        }

        void Update()
        {
            PlayerInput();
        }
    public void PlayerInput()
    {
        float keyBoardHorizontal = Input.GetAxis("HorizontalUI");
        float keyBoardVertical = Input.GetAxis("VerticalUI");
        if (keyBoardHorizontal != 0 || keyBoardVertical != 0)
        {
            touchInput = false;
            TankMovement(keyBoardHorizontal, keyBoardVertical);
        }
        else
            touchInput = true;
        if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
        {
            KeyboardInput = false;
            float touchHorizontal = joystick.Horizontal;
            float touchVertical = joystick.Vertical;
            TankMovement(touchHorizontal, touchVertical);
        }
        else
            KeyboardInput = true;

    }

    private void TankMovement(float horizontal,float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        Debug.Log("horizontal: " + horizontal + " vertical:" + vertical);
        tankRb.MovePosition(tankRb.position + movement * mvtSpeed * Time.deltaTime);
        rotation = new Vector3(horizontal, 0, vertical) * rotatingSpeed;
        tankRb.transform.rotation = Quaternion.LookRotation(rotation);
        

    }
}
