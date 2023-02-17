using UnityEngine;

public class LeaveState : BaseState
{
    public override void Enter()
    {
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
        CheckDistance();
    }

    public override void Exit() 
    { 

    }

    void CheckDistance()
    {
        if (npc.Agent.hasPath && npc.Agent.remainingDistance <= 0.2f)
        {
            GameEvents.onAIWaypointReached?.Invoke();
        }
    }
}
