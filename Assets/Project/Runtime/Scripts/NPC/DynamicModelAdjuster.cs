using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DynamicModelAdjuster : MonoBehaviour,INPC
{
    [SerializeField]
    private NavMeshAgent agent;
    private float heightInCentimeters;




    public void AdjustBaseOffset(int height)
    {
        heightInCentimeters = height;

        // Calculate the current scale factor based on the height
        float scaleFactor = transform.localScale.y * heightInCentimeters / 100.0f;

        // Calculate the new base offset based on the scale factor, agent's height, and current base offset
        float adjustedBaseOffset = scaleFactor * agent.baseOffset;

        // Scale the model based on the scale factor
        transform.localScale = new Vector3(1, scaleFactor, 1);

        // Update the base offset for the NavMeshAgent
        agent.baseOffset = adjustedBaseOffset;
    }


}
