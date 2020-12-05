using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    //Values--------------------------
    public float mvtSpeed, rotatingSpeed, health;
    private Vector3 rotation;

    //coloring---------------------------------
    public Renderer[] renderers;

    private Color tankColor;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyMovement()
    {

    }
    public void SetEnemyDetails(TankModel model)
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
                tankColor = Color.cyan;
                break;
            default:
                Debug.LogWarning("Please choose a color from the dropdown");
                return;
        }
        for (int i = 0; i < renderers.Length; i++)
            renderers[i].material.color = tankColor;

    }

}
