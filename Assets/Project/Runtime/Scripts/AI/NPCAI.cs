using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour, INPC
{

  private StateMachine stateMachine;
  private NavMeshAgent agent;
  private bool approvedForEntry;
  private NPCInformation npcInformation;
  private NPCIDData npcIDData;
  public NavMeshAgent Agent
  {
    get => agent;
  }

  public StateMachine StateMachine
  {
    get => stateMachine;
  }

  public bool ApprovedForEntry
  {
    get => approvedForEntry;
  }

  public NPCInformation NPCInformation
  {
    get => npcInformation;
  }

  public NPCIDData NPCIDData
  {
    get => npcIDData;
  }

  public Path path;

  // Start is called before the first frame update
  void Start()
  {
    path = FindObjectOfType<Path>();
    stateMachine = GetComponent<StateMachine>();
    agent = GetComponent<NavMeshAgent>();
    npcInformation = GetComponent<NPCInformation>();
    npcIDData = GetComponent<NPCIDData>();
    stateMachine.Initialise();
  }

  private void OnEnable()
  {
    GameEvents.onNPCDocumentsChecked += CheckApproval;
  }

  private void OnDisable()
  {
    GameEvents.onNPCDocumentsChecked -= CheckApproval;
  }


  void CheckApproval(bool approved)
  {
    approvedForEntry = approved;
  }

  public void OverrideApproval(bool overrideApproval)
  {
    approvedForEntry = overrideApproval;
  }
}
