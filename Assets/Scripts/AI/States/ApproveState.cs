using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApproveState : BaseState
{
    public override void Enter()
    {
        npc.Agent.SetDestination(npc.path.approveWaypoint.position);
        Debug.Log("Entering ApproveState");
    }

    public override void Perform()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting ApproveState");
    }
}
