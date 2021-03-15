using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : EnemyStateMachine
{
    private Collider[] players;
    public override void OnEnterState()
    {
        base.OnEnterState();
        Debug.Log(gameObject.name + " : Entered Attack State");
        players = Physics.OverlapSphere(transform.position, attackingRange, player);
        if (players[0] != null)
        {
            enemy.MoveTurret(players[0].transform);
            enemy.ShootDelay(1f);
        }
        else
            Debug.Log("player not detected");

    }
    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log(gameObject.name + " : Exiting Attack State");

    }
}
