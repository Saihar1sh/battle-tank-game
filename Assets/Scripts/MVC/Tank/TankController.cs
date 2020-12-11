using UnityEngine;

public class TankController 
{
    public TankController(TankView tankPrefab, TankModel tankModel, Vector3 _pos, Quaternion _rotation)
    {
        TankView = GameObject.Instantiate<TankView>(tankPrefab, _pos, _rotation);
        TankView.GetTankController(this);
        TankView.SetTankDetails(tankModel);
        TankService.Instance.tanks.Add(TankView);
    }
    public TankController(EnemyView enemyPrefab, TankModel tankModel, Vector3 _pos, Quaternion _rotation)
    {
        EnemyView  = GameObject.Instantiate<EnemyView>(enemyPrefab, _pos, _rotation);
        EnemyView.SetEnemyDetails(tankModel);
        TankService.Instance.enemyTanks.Add(EnemyView);

    }


    public void TankMovement(Vector3 movementInput, Rigidbody tankRb, float mvtSpeed, float rotatingSpeed)
    {
        //Debug.Log("horizontal: " + horizontal + " vertical:" + vertical);
        tankRb.MovePosition(tankRb.position + movementInput * mvtSpeed * Time.deltaTime);
        Vector3 rotation = movementInput * rotatingSpeed;
        tankRb.transform.rotation = Quaternion.LookRotation(rotation);
    }

    public void SetTankColor(TankColor _color, Renderer[] renderers)
    {
        Color tankColor;
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
                tankColor = Color.cyan;
                break;
            case TankColor.Purple:
                tankColor = new Vector4(0, 2, 0, 1);
                break;
            default:
                Debug.LogWarning("Please choose a color from the dropdown");
                return;
        }
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = tankColor;

    }


    public TankView TankView { get; }
    public EnemyView EnemyView { get; }


}
