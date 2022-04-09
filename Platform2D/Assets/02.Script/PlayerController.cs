using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Jump,
        Fall,
        Dash,
    }
    public enum IdleState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public enum RunState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public enum JumpState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public enum FallState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public enum DashState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
   
    public PlayerState state;
    public IdleState idleState;
    public RunState runState;
    public JumpState jumpState;
    public FallState fallState;
    public DashState dashState;




    public float moveSpeed = 2;
    public float jumpForce = 0.1f;
    public int direction
    {
        set
        {
            if (value < 0)
            {
                _direction = -1;
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }

            else if (value > 0) transform.eulerAngles = Vector3.zero;
        }

        get { return _direction; }
    }

    private Rigidbody2D rb;
    private int _direction; // +1 :right , -1 = left
    private float moveInputOffset = 0.1f;
    private Vector2 move;
    private Animator animator;
    private GroundDetector groundDetector;
    private float jumpTime = 0.1f;
    private float jumpTimer;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundDetector = GetComponent<GroundDetector>();
    }
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        if (h < 0) direction = -1;
        else if (h > 0) direction = 1;

        if (Mathf.Abs(h) > moveInputOffset)
        {
            move.x = h;
            if (state == PlayerState.Idle )
                ChangePlayerState(PlayerState.Run);
        }
        else
        {
            move.x = 0;
            if (state == PlayerState.Run) ChangePlayerState(PlayerState.Idle); ;
        }

        if (Input.GetButton("Jump"))
        {

            if (state != PlayerState.Jump && state != PlayerState.Fall && groundDetector.isDetected)
            {
                ChangePlayerState(PlayerState.Jump);
            }
        }
        UpdatePlayerState();
    }
    private void FixedUpdate()
    {
        rb.position += new Vector2(move.x * moveSpeed, 0) * Time.fixedDeltaTime;
    }

    public void ChangePlayerState(PlayerState newState)
    {
        if (state == newState) return;

        switch (state) // 이전상태 하위머신 초기화
        {
            case PlayerState.Idle:
                idleState = IdleState.Idle;
                break;
            case PlayerState.Run:
                runState = RunState.Idle;
                break;
            case PlayerState.Jump:
                jumpState = JumpState.Idle;
                break;
            case PlayerState.Fall:
                fallState = FallState.Idle;
                break;
            case PlayerState.Dash:
                dashState = DashState.Idle;
                break;
            default:
                break;
        }

        state = newState; // 현재상태 바꿈
        
        switch (state) // 현재상태 하위머신 준비
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Run:
                break;
            case PlayerState.Jump:
                jumpState = JumpState.Prepare;
                break;
            case PlayerState.Fall:
                fallState = FallState.Prepare;
                break;
            case PlayerState.Dash:
                dashState= DashState.Prepare;
                break;
            default:
                break;
        }
    }

    private void UpdatePlayerState()
    {
        switch (state)
        {
            case PlayerState.Idle:
                UpdateIdleState();
                break;
            case PlayerState.Run:
                UpdateRunState();
                break;
            case PlayerState.Jump:
                UpdateJumpState();
                break;
            case PlayerState.Fall:
                UpdateFallState();
                break;
            case PlayerState.Dash:
                UpdateDashState();
                break;
            default:
                break;
        }

    }
    private void UpdateJumpState()
    {
        switch (jumpState)
        {
            case JumpState.Idle:
                break;
            case JumpState.Prepare:
                animator.Play("Jump");
                rb.velocity = new Vector2(rb.velocity.x, 0); //속도 초기화
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimer = jumpTime;
                jumpState++; // = JumpState.Casting;
                break;
            case JumpState.Casting:
                if(!groundDetector.isDetected) jumpState++;
                else if(jumpTimer < 0) ChangePlayerState(PlayerState.Idle);
                jumpTimer -= Time.deltaTime;
                break;
            case JumpState.OnAction:
                if (rb.velocity.y < 0)jumpState++;                
                break;
            case JumpState.Finish:
                ChangePlayerState(PlayerState.Fall);
                break;
            default:
                break;
        }

    }
    private void UpdateFallState()
    {
        switch (fallState)
        {
            case FallState.Idle:
                break;
            case FallState.Prepare:
                animator.Play("Fall");
                fallState++;
                break;
            case FallState.Casting:
                fallState++;
                break;
            case FallState.OnAction:
                if(groundDetector.isDetected) fallState++;
                break;
            case FallState.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }

    }
    private void UpdateIdleState()
    {
        switch (idleState)
        {
            case IdleState.Idle:
                animator.Play("Idle");
                idleState++;
                break;
            case IdleState.Prepare:
                idleState++;
                break;
            case IdleState.Casting:
                break;
            case IdleState.OnAction:
                break;
            case IdleState.Finish:
                break;
            default:
                break;
        }

    }
    private void UpdateRunState()
    {
        switch (runState)
        {
            case RunState.Idle:
                animator.Play("Run");
                runState++;
                break;
            case RunState.Prepare:
                runState++;
                break;
            case RunState.Casting:
                break;
            case RunState.OnAction:
                break;
            case RunState.Finish:
                break;
            default:
                break;
        }

    }

    private void UpdateDashState()
    {
        switch (dashState)
        {
            case DashState.Idle:
                animator.Play("Dash");
                dashState++;
                break;
            case DashState.Prepare:
                break;
            case DashState.Casting:
                break;
            case DashState.OnAction:
                break;
            case DashState.Finish:
                break;
            default:
                break;
        }

    }

}
