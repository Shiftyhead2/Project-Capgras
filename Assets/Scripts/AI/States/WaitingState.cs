using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingState : BaseState
{
    private DenyState denyState = new DenyState();
    private ApproveState approveState = new ApproveState();
    
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
        if(approved)
        {
            npc.StateMachine.ChangeState(approveState);
        }
        else
        {
            //In case we are not approved
            npc.StateMachine.ChangeState(denyState);
        }
    }
}
