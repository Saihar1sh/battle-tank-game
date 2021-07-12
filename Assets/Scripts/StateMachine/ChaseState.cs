using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyStateMachine
{
    private Collider[] players;

    public override void OnEnterState()
    {
        base.OnEnterState();
        Debug.Log(gameObject.name + " : Entered Chase State");
        players = Physics.OverlapSphere(transform.position, chasingRange, player);
        if (players[0] != null)
            agent.SetDestination(players[0].transform.position);

    }
    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log(gameObject.name + " : Exiting Chase State");
        
    }
}
