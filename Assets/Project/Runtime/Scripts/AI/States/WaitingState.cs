using UnityEngine;

public class WaitingState : BaseState
{
    private LeaveState leaveState = new LeaveState();
    
    public override void Enter()
    {
        GameEvents.onNPCDocumentsChecked += CheckState;
        GameEvents.onSituationChoice += CheckChoice;
        Debug.Log("Entered WaitingState");
    }

    public override void Perform()
    {
        
    }

    public override void Exit()
    {
        GameEvents.onNPCDocumentsChecked -= CheckState;
        GameEvents.onSituationChoice -= CheckChoice;
        Debug.Log("Exited WaitingState");
    }


    void CheckState(bool approved)
    {
        if(npc.ApprovedForEntry == false)
        {
            if (ShouldBeg(npc.NPCInformation.begChance))
            {
                Debug.Log("NPC is begging!");
                GameEvents.onNPCSituation?.Invoke(npc.NPCInformation.beggingObject);
            }
            else
            {
                ChangeState();
            }
        }
        else
        {
            ChangeState();
        }
    }

    void CheckChoice(int choiceID)
    {
        if(choiceID == 0)
        {
            npc.OverrideApproval(true);
        }
        else if(choiceID == 1)
        {
            npc.OverrideApproval(false);
        }
        GameEvents.onSituationResolved?.Invoke();
        ChangeState();
    }

    bool ShouldBeg(float chance)
    {
        float currentChance = Random.Range(0f, 1f);
        Debug.Log($"Current chance to beg is: {currentChance * 100f:0}% \n The chance for the NPC to beg is: {chance * 100f:0}%");
        if(currentChance <= chance)
        {
            return true;
        }
        return false;
    }

    void ChangeState()
    {
        npc.StateMachine.ChangeState(leaveState);
    }
}
