using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IABasic : MonoBehaviour
{
    public State currentState;


    public NavMeshAgent nav;


    public PlayerStats currentTarget;

    [Header("A.I. Properties")]
    public float detectionRadius = 20;

    //The higher or lower the angles are, the greater detection
    public float maxDetectionAngle = 50;
    public float minDetectionAngle = -50;

    private void Awake()
    {
        currentTarget = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleCurrentAction();
    }

    public void HandleCurrentAction()
    {
        if (currentState != null)
        {
            State next = currentState.RunCurrentState();

            if (next != null)
            {
                SwitchToNextState(next);
            }
        }
    }

    private void SwitchToNextState(State state)
    {
        currentState = state;
    }
}
