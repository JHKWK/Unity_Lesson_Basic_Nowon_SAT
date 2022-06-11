using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Move,
    Jump,
    Attack
}
public class PlayerStateMachineManager : MonoBehaviour
{
    public PlayerStateMachine_Jump jumpMachine;
    public PlayerState state;

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) &&
            jumpMachine.state == PlayerStateMachine.State.Idle &&
            jumpMachine.isExecuteOK() )
        {
            jumpMachine.Excute();
            state = PlayerState.Jump;
        }

        jumpMachine.UpdateState();
     
    }

    private void FixedUpdate()
    {
        jumpMachine.FixedUpdate();
    }
}
