﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyView))] 
public class EnemyStateMachine : MonoBehaviour
{
    protected EnemyView enemy;
    protected NavMeshAgent agent;

    protected float chasingRange = 15f, attackingRange = 9f;

    protected LayerMask player;
    protected StatesController statesController;
    protected void Awake()
    {
        enemy = GetComponent<EnemyView>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {

    }
    private void OnEnable()
    {
        statesController = GetComponent<StatesController>();
        chasingRange = statesController.chaseRange;
        attackingRange = statesController.attackRange;
        player = statesController.whatIsPlayer;

    }
    public virtual void OnEnterState()
    {
        this.enabled = true;
    }
    public virtual void OnExitState()
    {
        this.enabled = false;
    }
    
}
