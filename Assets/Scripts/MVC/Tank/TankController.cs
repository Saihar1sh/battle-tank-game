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

    public void SetTankColor(Color _color, Renderer[] renderers)
    {
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = _color;

    }

    public void ApplyHealth(float amount)
    {

    }

    public TankView TankView { get; }
    public EnemyView EnemyView { get; }


}
