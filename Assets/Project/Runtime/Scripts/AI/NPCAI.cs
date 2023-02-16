using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{

    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent
    {
        get => agent;
    }

    public StateMachine StateMachine
    {
        get => stateMachine;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
