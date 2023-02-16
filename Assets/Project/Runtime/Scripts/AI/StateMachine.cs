using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public EnterState enterState;

    public void Initialise()
    {
        //setup default state
        enterState = new EnterState();
        ChangeState(enterState);
    }

   
    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        if(activeState != null)
        {
            activeState.Exit();
        }
        activeState = newState;

        if(activeState != null)
        {
            activeState.stateMachine = this;
            activeState.npc = GetComponent<NPCAI>();
            activeState.Enter();
        }
    }
}
