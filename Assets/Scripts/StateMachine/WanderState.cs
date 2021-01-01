using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderState : EnemyStateMachine
{

    private float stoppingDistance = 1.5f;
    private Vector3 destination;
    public override void OnEnterState()
    {
        Debug.Log(gameObject.name + " : Entered Wander State");

        base.OnEnterState();
        if (NeedsDestination())
        {
            GetDestination();
            Debug.Log("getting dest");
        }

    }
    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log(gameObject.name + " : Exiting Wander State");

    }
    private void GetDestination()
    {
        destination  = new Vector3(Random.Range(-38f, 26f), transform.position.y, Random.Range(-30f, 30f));
        //Debug.Log(gameObject.name + " : " + destination);
        agent.SetDestination(destination);
    }

    public bool NeedsDestination()
    {
           if (destination == Vector3.zero)
               return true;

        float distance = Vector3.Distance(transform.position, destination);
           if (distance <= stoppingDistance)
               return true;

       return false;
    }

}
