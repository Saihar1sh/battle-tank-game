using UnityEngine;

public class TankController 
{
    //movement values--------------------------
    public float mvtSpeed;

    private float touchHorizontal, touchVertical, keyBoardHorizontal, keyBoardVertical;
    private Vector3 rotation;
    private bool touchInput = true, KeyboardInput = true;

    //references-------------------------------
    private Joystick joystick;
    private Rigidbody tankRb;

/*   protected override void Awake()
    {
        base.Awake();
        tankRb = GetComponent<Rigidbody>();
        joystick = FindObjectOfType<FixedJoystick>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        PlayerInput();
        PlayerTouchInput();
    }
   
*/    private void PlayerTouchInput(Vector3 playerCurrentPos)
    {
        if (touchInput)
        {
            touchHorizontal = joystick.Horizontal * mvtSpeed;
            touchVertical = joystick.Vertical * mvtSpeed;
            TankTouchMovement(playerCurrentPos);
            TankTouchRotation();
        }
    }
    private void TankTouchMovement(Vector3 currentPos)
    {
        Vector3 movement = new Vector3(touchHorizontal, 0, touchVertical);
        tankRb.MovePosition(currentPos + movement * Time.deltaTime);

    }
    private void TankTouchRotation()
    {
        if (joystick.Direction.x != 0 && joystick.Direction.y != 0)
        {
            KeyboardInput = false;
            rotation = new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
        }
        else
            KeyboardInput = true;
        tankRb.transform.rotation = Quaternion.LookRotation(rotation , Vector3.up);

    }
    private void PlayerInput(Vector3 playerCurrentPos)
    {
        if (KeyboardInput)
        {
            keyBoardHorizontal = Input.GetAxis("HorizontalUI") * mvtSpeed;
            keyBoardVertical = Input.GetAxis("VerticalUI") * mvtSpeed;
            TankMovement(playerCurrentPos);
            TankRotation();
        }
    }
    private void TankMovement(Vector3 currentPos)
    {
        Vector3 movement = new Vector3(keyBoardHorizontal, 0, keyBoardVertical);
        tankRb.MovePosition(currentPos + movement * Time.deltaTime);
    }
    private void TankRotation()
    {
        if (keyBoardHorizontal != 0 || keyBoardVertical != 0)
        {
            touchInput = false;
            rotation = new Vector3(keyBoardHorizontal, 0, keyBoardVertical);
        }
        else
            touchInput = true;
        tankRb.transform.rotation = Quaternion.LookRotation(rotation, Vector3.up);

    }

}
