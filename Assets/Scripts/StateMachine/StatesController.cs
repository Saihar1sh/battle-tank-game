using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatesController : MonoBehaviour
{
    public LayerMask whatIsPlayer;

    public float attackRange = 9f, chaseRange = 15f;

    //States
    private WanderState wanderState;
    private ChaseState chaseState;
    private AttackState attackState;

    //private TankState _currentState;
    private EnemyStateMachine currentState;
    private bool playerInChaseRange = false, playerInAttackRange = false;
    private void Awake()
    {
        wanderState = GetComponent<WanderState>();
        chaseState = GetComponent<ChaseState>();
        attackState = GetComponent<AttackState>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentState = wanderState;
    }

    // Update is called once per frame
    void Update()
    {
        //recording Data
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (playerInAttackRange)
            playerInChaseRange = false;
        else
            playerInChaseRange = Physics.CheckSphere(transform.position, chaseRange, whatIsPlayer);



        //changing States
        ChangeStateTo(currentState);

        if (playerInChaseRange)
            currentState = chaseState;

        else if (playerInAttackRange)
            currentState = attackState;
        else if (!playerInAttackRange && !playerInChaseRange)
            currentState = wanderState;
        else
            currentState = wanderState;


    }

    private void ChangeStateTo(EnemyStateMachine state)
    {
        currentState.OnExitState();
        currentState = state;
        currentState.OnEnterState();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
