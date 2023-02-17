using UnityEngine;

public class WaitingState : BaseState
{
    private LeaveState leaveState = new LeaveState();
    
    public override void Enter()
    {
        GameEvents.onNPCDocumentsChecked += CheckState;
        Debug.Log("Entered WaitingState");
    }

    public override void Perform()
    {
        
    }

    public override void Exit()
    {
        GameEvents.onNPCDocumentsChecked -= CheckState;
        Debug.Log("Exited WaitingState");
    }


    void CheckState(bool approved)
    {
        npc.StateMachine.ChangeState(leaveState);
    }
}
