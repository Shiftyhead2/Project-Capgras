using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterState : BaseState
{
    private WaitingState waitingState = new WaitingState();

    public override void Enter()
    {
        Debug.Log("Entered EnterState");
        npc.Agent.SetDestination(npc.path.middleWaypoint.position);
    }

    public override void Perform()
    {
        PerformEnter();
    }

    public override void Exit()
    {
        Debug.Log("Exited EnterState");
    }

    void PerformEnter()
    {

        if(npc.Agent.remainingDistance < 0.2f)
        {
            //We reached the first checkpoint
            npc.StateMachine.ChangeState(waitingState);
            
        }
        
    }
}
