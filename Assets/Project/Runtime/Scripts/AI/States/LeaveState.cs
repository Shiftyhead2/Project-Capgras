using UnityEngine;

public class LeaveState : BaseState
{
    public override void Enter()
    {

        Debug.Log("Entering LeaveState");
        if (npc.ApprovedForEntry)
        {
            npc.Agent.SetDestination(npc.path.approveWaypoint.position);
        }
        else
        {
            npc.Agent.SetDestination(npc.path.denyWaypoint.position);
        }
    }

    public override void Perform()
    {
       
    }

    public override void Exit() 
    {
        Debug.Log("Exitting LeaveState");
    }
}
