using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState playerState;
    public State state;


    public bool isFinished
    {
        get => state == State.Finish ? true : false;
    }

    protected PlayerStateMachineManager manager;
    protected PlayerAnimator playerAnimator;
    protected float delay;
    protected float delayTimer;

    protected virtual void Awake()
    {
        manager = GetComponent<PlayerStateMachineManager>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    public virtual bool isExecuteOK()
    {
        return true;
    }

    public virtual void Excute()
    {
        state = State.Prepare;
    }

    public virtual PlayerState UpdateState()
    {
        PlayerState nextStage = playerState;
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:
                state++;
                break;
            case State.OnDelay:
                state++;
                break;
            case State.Casting:
                state++;
                break;
            case State.OnAction:
                state++;
                break;
            case State.Finish:
                //Set nextState to change other state
                break;
            default:
                break;
        }

        return nextStage;
    }

    public virtual void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Prepare:

                break;
            case State.OnDelay:

                break;
            case State.Casting:

                break;
            case State.OnAction:

                break;
            case State.Finish:

                break;
            default:
                break;
        }
    }

    public virtual void ForceStop()
    {
        state = State.Idle;
    }
    public enum State
    {
        Idle,
        Prepare,
        OnDelay,
        Casting,
        OnAction,
        Finish,
    }
}
