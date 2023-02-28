using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour,INPC
{

    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private bool approvedForEntry;
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

    public Path path;
    // Start is called before the first frame update
    void Start()
    {
        path = FindObjectOfType<Path>();
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
    }

    private void OnEnable()
    {
        GameEvents.onNPCDocumentsChecked += checkApproval;
        GameEvents.onAIWaypointReached += Despawn;
    }

    private void OnDisable()
    {
        GameEvents.onNPCDocumentsChecked -= checkApproval;
        GameEvents.onAIWaypointReached -= Despawn;
    }


    void checkApproval(bool approved)
    {
        approvedForEntry = approved;
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}
