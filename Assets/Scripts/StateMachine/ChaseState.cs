using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyStateMachine
{
    private Collider[] players;

    public override void OnEnterState()
    {
        base.OnEnterState();
        players = Physics.OverlapSphere(transform.position, chasingRange, player);
        if (players[0] != null)
        {
            agent.SetDestination(players[0].transform.position);
            enemy.MoveTurret(defaultTransform);
        }

    }
    public override void OnExitState()
    {
        base.OnExitState();


    }
}
