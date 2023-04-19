/// <summary>
/// A base abstract class for creating states
/// </summary>
public abstract class BaseState
{
  public NPCAI npc;
  public StateMachine stateMachine;
  /// <summary>
  /// Performed when the state first runs
  /// </summary>
  public abstract void Enter();
  /// <summary>
  /// Performed every frame
  /// </summary>
  public abstract void Perform();
  /// <summary>
  /// Performed on an active state before we change into a new state
  /// </summary>
  public abstract void Exit();
}
