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
        if (npc.ApprovedForEntry == false)
        {
            if (npc.NPCIDData.CurrentAmountOfFalseData == 0 && ShouldBegOrBribe(npc.NPCInformation.begChance))
            {
                Debug.Log("NPC is begging!");
                GameEvents.onNPCSituation?.Invoke(npc.NPCInformation.beggingObject);
            }
            else if (npc.NPCIDData.CurrentAmountOfFalseData > 0 && ShouldBegOrBribe(npc.NPCInformation.bribeChance))
            {
                Debug.Log("NPC is trying to bribe the player!");
                GameEvents.onNPCSituation?.Invoke(npc.NPCInformation.bribeObject);
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
        switch (choiceID)
        {
            case 1:
                npc.OverrideApproval(false);
                break;
            case 2:
                npc.OverrideApproval(false);
                break;
            default:
                GameEvents.updateApprovalCount?.Invoke();
                npc.OverrideApproval(true);
                break;
        }
        GameEvents.onSituationResolved?.Invoke();
        ChangeState();
    }

    bool ShouldBegOrBribe(float chance)
    {
        float currentChance = Random.Range(0f, 1f);
        Debug.Log($"Current chance to beg/bribe is: {currentChance * 100f:0}% \n The chance for the NPC to beg/bribe is: {chance * 100f:0}%");
        if (currentChance <= chance)
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
