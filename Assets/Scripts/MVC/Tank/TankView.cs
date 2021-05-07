using UnityEngine;
using System.Collections;
using System;

public class TankView : MonoBehaviour, IDamagable
{

    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, maxHealth, tankExplosionDelay = 1f;
    [Tooltip("Reloading Time")]
    public float shootDelay = 1f;
    //private Vector3 rotation;
    public float currentHealth;
    private bool touchInput = true, KeyboardInput = true;
    private bool canShoot = true;

    [HideInInspector]
    public bool fireBulletBool = false;

    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;

    //references-------------------------------
    private Joystick mvtJoystick, shootJoystick;
    private Rigidbody tankRb;
    [SerializeField]
    private Transform tankTurret, tankShootPos;
    private TankController tankController;
    private HealthBar healthBar;

    private bool godMode = false;

    private void Awake()
    {
        //referencing
        tankRb = GetComponent<Rigidbody>();
        mvtJoystick = FindObjectOfType<FixedJoystick>();
        shootJoystick = FindObjectOfType<FloatingJoystick>();
        healthBar = GetComponentInChildren<HealthBar>();
    }
    private void OnEnable()
    {
        //initialsing
        tankController = new TankController(this);
        TankService.Instance.tanks.Add(this);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        UIManager.Instance.InstantiatePlayerRef(gameObject);

    }
    private void OnDisable()
    {
        TankService.Instance.tanks.Remove(this);
    }
    private void Start()
    {
    }

    private void Update()
    {
        CheckHealth();
        CheckCanShoot();
        fireBulletBool = BulletService.Instance.fireAmmoBool;
    }


    private void FixedUpdate()
    {
        PlayerInput();
        MoveTurret();
    }

    public void GetTankController(TankController _tankController)
    {
        this.tankController = _tankController;
    }

    private void CheckCanShoot()
    {
        if (canShoot)
        {
            if (Mathf.Abs(shootJoystick.Direction.x) >= 0.7 || Mathf.Abs(shootJoystick.Direction.y) >= 0.7 && gameObject.activeInHierarchy)
            {
                StartCoroutine(ShootBulletDelay(shootDelay));
            }
        }
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
            Vector3 movementInput = new Vector3(keyBoardHorizontal, 0, keyBoardVertical);
            tankController.TankMovement(movementInput, tankRb, mvtSpeed, rotatingSpeed);
        }
        else
            touchInput = true;
        if (mvtJoystick.Direction.x != 0 && mvtJoystick.Direction.y != 0)
        {
            KeyboardInput = false;
            float touchHorizontal = mvtJoystick.Horizontal;
            float touchVertical = mvtJoystick.Vertical;
            Vector3 movementInput = new Vector3(touchHorizontal, 0, touchVertical);
            tankController.TankMovement(movementInput, tankRb, mvtSpeed, rotatingSpeed);
        }
        else
            KeyboardInput = true;
    }
    public void SetTankDetails(TankModel model)
    {
        mvtSpeed = model.mvtSpeed;
        rotatingSpeed = model.rotatingSpeed;
        maxHealth = model.health;
        Color color = model.TankColor;
        //Debug.Log("color :" + color);
        tankController.SetTankColor(color, renderers);
        shootDelay = model.reloadTime;
    }

    private void Shoot()
    {
        //Debug.Log("shoot");
        if (fireBulletBool)
        {
            BulletService.Instance.InstantiateFireBullet(tankShootPos);
            return;
        }
        else
            BulletService.Instance.InstantiateBullet(tankShootPos);
    }

    public void ModifyHealth(float amount)
    {
        if (godMode == false)
            currentHealth += amount;      //-----------------------
        healthBar.SetHealth(currentHealth);
    }
    private void CheckHealth()
    {
        if (currentHealth <= 0)
            DestroyTank();
    }
    public void SetCurrentHealth(float k)
    {
        currentHealth = k;
    }
    public float ReturnCurrentHealth()
    {
        return currentHealth;
    }
    public void PlayerCheatModeActivate()
    {
        godMode = true;
        shootDelay = 0;
        BulletService.Instance.fireAmmoBool = true;
    }
    public void PlayerCheatModeDisabled()
    {
        godMode = false;
        shootDelay = 1;
        BulletService.Instance.fireAmmoBool = false;
    }

    public void DestroyTank()
    {
        Particles.Instance.CommenceTankExplosion(transform);
        TankService.Instance.SpawnBustedTank(transform);
        ServiceEvents.Instance.OnPlayerDeathInVoke();
        gameObject.SetActive(false);
        TankService.Instance.tanks.Remove(this);
        PoolService.Destroy(gameObject);

    }

    //Coroutines
    IEnumerator ShootBulletDelay(float secs)
    {
        canShoot = false;
        Shoot();
        yield return new WaitForSeconds(secs);
        canShoot = true;
    }


}
