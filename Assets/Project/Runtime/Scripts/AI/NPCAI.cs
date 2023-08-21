using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

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
    async void Awake()
    {
        await InitializeAsync();
    }

    async UniTask InitializeAsync()
    {
        await UniTask.Yield(PlayerLoopTiming.Initialization);

        await UniTask.WhenAll(WaitForFindComponentAsync<Path>(),
            WaitForComponentAsync<StateMachine>(),
            WaitForComponentAsync<NavMeshAgent>(),
            WaitForComponentAsync<NPCInformation>(),
            WaitForComponentAsync<NPCIDData>());

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

    private async UniTask WaitForComponentAsync<T>() where T : Component
    {
        await UniTask.WaitUntil(() => GetComponent<T>() != null);
    }

    private async UniTask WaitForFindComponentAsync<T>() where T: Component
    {
        await UniTask.WaitUntil(() => FindObjectOfType<T>() != null);
    }
}
