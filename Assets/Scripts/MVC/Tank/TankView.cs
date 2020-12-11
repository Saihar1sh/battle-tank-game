using UnityEngine;
using System.Collections;

public class TankView : MonoBehaviour
{
    private bool movingTurret = true;

    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, health;
    public float tankExplosionDelay;
    //private Vector3 rotation;

    private bool touchInput = true, KeyboardInput = true;

    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;

    //references-------------------------------
    private Joystick mvtJoystick, shootJoystick;
    private Rigidbody tankRb;
    [SerializeField]
    private Transform tankTurret, tankShootPos;
    [SerializeField]
    private Bullet BulletPrefab;
    private TankController TankController;

    private void Awake()
    {
        tankRb = GetComponent<Rigidbody>();
        mvtJoystick = FindObjectOfType<FixedJoystick>();
        shootJoystick = FindObjectOfType<FloatingJoystick>();
    }
    private void Update()
    {
        if (Mathf.Abs(shootJoystick.Direction.x) >= 0.7 || Mathf.Abs(shootJoystick.Direction.y) >= 0.7)
        {
            Shoot();
        }

    }
    private void FixedUpdate()
    {
        PlayerInput();
        MoveTurret();
    }
    public void GetTankController(TankController tankController)
    {
        TankController = tankController;
    }

    private void MoveTurret()
    {
        if (shootJoystick.Direction.x != 0 && shootJoystick.Direction.y != 0)
        {
            movingTurret = true;
            Vector3 turretRotation = new Vector3(shootJoystick.Direction.x, 0, shootJoystick.Direction.y) * rotatingSpeed;
            tankTurret.rotation = Quaternion.LookRotation(turretRotation);

        }
        else
            movingTurret = false;


    }
    private void OnDrawGizmos()
    {
        if(movingTurret)
        Gizmos.DrawLine(tankTurret.position, shootJoystick.Direction);
    }

    //records Player's Inputs and makes sure when one input device is giving input other doesn't give input.
    public void PlayerInput()
    {
        float keyBoardHorizontal = Input.GetAxis("HorizontalUI");
        float keyBoardVertical = Input.GetAxis("VerticalUI");
        if (keyBoardHorizontal != 0 || keyBoardVertical != 0)
        {
            touchInput = false;
            TankController.TankMovement(new Vector3(keyBoardHorizontal, 0, keyBoardVertical),tankRb, mvtSpeed, rotatingSpeed);
        }
        else
            touchInput = true;
        if (mvtJoystick.Direction.x != 0 && mvtJoystick.Direction.y != 0)
        {
            KeyboardInput = false;
            float touchHorizontal = mvtJoystick.Horizontal;
            float touchVertical = mvtJoystick.Vertical;
            TankController.TankMovement(new Vector3(touchHorizontal, 0, touchVertical), tankRb, mvtSpeed, rotatingSpeed);
        }
        else
            KeyboardInput = true;
    }

    public void SetTankDetails(TankModel model)
    {
        mvtSpeed = model.mvtSpeed;
        rotatingSpeed = model.rotatingSpeed;
        health = model.health;
        TankColor color = model.TankColor;
        //Debug.Log("color :" + color);
        TankController.SetTankColor(color, renderers);
    }

    private void Shoot()
    {
        //Debug.Log("shoot");
        Instantiate(BulletPrefab, tankShootPos.position, Quaternion.identity);


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<EnemyView>() != null)
        {
            DestroyTank();
        }
    }
    private void OnDestroy()
    {
        TankService.Instance.DestroyEverything();
    }
    public void DestroyTank()
    {
        Particles.Instance.CommenceTankExplosion(transform);
        enabled = false;
        StartCoroutine(TankExplosionDelay());
    }
    
    IEnumerator TankExplosionDelay()
    {
        yield return new WaitForSeconds(tankExplosionDelay);
        TankService.Instance.tanks.Remove(this);
        Destroy(gameObject);
    }
}
