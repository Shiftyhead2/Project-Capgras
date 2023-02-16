using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DenyState : BaseState
{
    public override void Enter()
    {
        npc.Agent.SetDestination(npc.path.denyWaypoint.position);
        Debug.Log("Entering DenyState!");
    }

    public override void Perform()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting DenyState!");
    }
}
