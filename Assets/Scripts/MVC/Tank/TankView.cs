using UnityEngine;

public class TankView : MonoBehaviour
{
    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, health;
    private Vector3 rotation;

    private bool touchInput = true, KeyboardInput = true;

    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;

    //references-------------------------------
    private Joystick mvtJoystick, shootJoystick;
    private Rigidbody tankRb;
    [SerializeField]
    private Transform tankTurret;

    private void Awake()
    {
        tankRb = GetComponent<Rigidbody>();
        mvtJoystick = FindObjectOfType<FixedJoystick>();
        shootJoystick = FindObjectOfType<FloatingJoystick>();
    }
    private void Update()
    {
        if (Mathf.Abs(shootJoystick.Direction.x) >= 0.7 || Mathf.Abs(shootJoystick.Direction.y) >= 0.7)
            shoot();
        //SetTankColor();

    }
    private void FixedUpdate()
    {
        PlayerInput();
        MoveTurret();
    }

    private void MoveTurret()
    {
        if (shootJoystick.Direction.x != 0 && shootJoystick.Direction.y != 0)
        {
            Vector3 turretRotation = new Vector3(shootJoystick.Direction.x, 0, shootJoystick.Direction.y) * rotatingSpeed;
            tankTurret.rotation = Quaternion.LookRotation(turretRotation);
        }


    }

    //records Player's Inputs and makes sure when one input device is giving input other doesn't give input.
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
        if (mvtJoystick.Direction.x != 0 && mvtJoystick.Direction.y != 0)
        {
            KeyboardInput = false;
            float touchHorizontal = mvtJoystick.Horizontal;
            float touchVertical = mvtJoystick.Vertical;
            TankMovement(touchHorizontal, touchVertical);
        }
        else
            KeyboardInput = true;

    }

    private void TankMovement(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0, vertical);
        //Debug.Log("horizontal: " + horizontal + " vertical:" + vertical);
        tankRb.MovePosition(tankRb.position + movement * mvtSpeed * Time.deltaTime);
        rotation = new Vector3(horizontal, 0, vertical) * rotatingSpeed;
        tankRb.transform.rotation = Quaternion.LookRotation(rotation);
    }
    public void SetTankDetails(TankModel model)
    {
        mvtSpeed = model.mvtSpeed;
        rotatingSpeed = model.rotatingSpeed;
        health = model.health;
        TankColor color = model.TankColor;
        //Debug.Log("color :" + color);
        SetTankColor(color);
    }
   private void SetTankColor(TankColor _color)
    {
        switch (_color)
        {
            case TankColor.Green:
                tankColor = Color.green;
                break;
            case TankColor.Black:
                tankColor = Color.black;
                break;
            case TankColor.Blue:
                tankColor = Color.blue;
                break;
            case TankColor.Red:
                tankColor = Color.red;
                break;
            case TankColor.Cyan:
                tankColor = new Vector4(0,1,1,1);
                break;
            case TankColor.DarkGreen:
                tankColor = new Vector4(0,2,0,1);
                    break;
            default:
                Debug.LogWarning("Please choose a color from the dropdown");
                return;
        }
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = tankColor;

    }

    private void shoot()
    {
        Debug.Log("shoot");
    }
}
