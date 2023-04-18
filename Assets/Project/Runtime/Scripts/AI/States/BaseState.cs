public abstract class BaseState
{
  //Perfomed when the state first runs
  public NPCAI npc;
  public StateMachine stateMachine;
  public abstract void Enter();
  //Perfomed every frame
  public abstract void Perform();
  //Performed on an active state before we change into a new state
  public abstract void Exit();
}
