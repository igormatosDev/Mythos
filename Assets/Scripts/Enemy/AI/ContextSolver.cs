using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSolver : MonoBehaviour
{
    [SerializeField]
    private bool showGizmos = false;

    //gozmo parameters
    float[] interestGizmo = new float[0];
    Vector2 resultDirection = Vector2.zero;
    [SerializeField] private float rayLength = 2;

    private void Start()
    {
        interestGizmo = new float[8];
    }
    public Vector2 GetDirectionToMove(List<SteeringBehaviour> behaviours, AIData aiData)
    {
        float[] danger = new float[8];
        float[] interest = new float[8];

        //Loop through each behaviour
        foreach (SteeringBehaviour behaviour in behaviours)
        {
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
        }

        //subtract danger values from interest array
        for (int i = 0; i < 8; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
            //print($"interest[{i}]: {interest[i]} | danger[{i}]: {danger[i]}");
        }
        

        interestGizmo = interest;

        //get the average direction
        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            outputDirection += Directions.eightDirections[i] * interest[i];
        }
        outputDirection.Normalize();
        resultDirection = outputDirection;

        //return the selected movement direction
        return resultDirection;
    }

    public Vector2 GetDirectionToAvoid(List<SteeringBehaviour> behaviours, AIData aiData)
    {
        // I made this other version of GetDirectionToMove but this only finds the DANGER ZONES, so we can calculate direction to avoid moving

        float[] danger = new float[8];
        float[] interest = new float[8];

        foreach (SteeringBehaviour behaviour in behaviours)
        {
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
        }
        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            outputDirection += Directions.eightDirections[i] * Mathf.Clamp01(danger[i]);
        }
        outputDirection.Normalize();
        resultDirection = outputDirection;
        return resultDirection;
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying && showGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * rayLength);
        }
    }
}