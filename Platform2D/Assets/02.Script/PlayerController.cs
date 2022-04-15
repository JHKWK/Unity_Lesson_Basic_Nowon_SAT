using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum PlayerState
    {
        Idle,
        Run,
        Crouch,
        Jump,
        JumpAttack,
        Fall,
        Dash,
        Attack,
        DashAttack,
        Slide
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
    public enum CrouchState
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
    public enum JumpAttackState
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
    public enum AttackState
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
    public enum DashAttack
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public enum Slide
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
    public CrouchState crouchState;
    public JumpState jumpState;
    public JumpAttackState jumpAttackState;
    public FallState fallState;
    public DashState dashState;
    public AttackState attackState;
    public DashAttack dashAttackState;
    public Slide slideState;

    public Vector2 moveVector;

    public float moveSpeed = 2;
    public float jumpForce = 0.1f;
    public float dashForce = 3;
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
    private Vector2 dashDir;
    private float moveInputOffset = 0.1f;
    private Vector2 move;
    private Animator animator;
    private GroundDetector groundDetector;

    private float jumpTime = 0.1f;
    private float jumpTimer;
    private float dashTime = 0.7f;
    private float dashTimer;
    private float attackTime = 0.6f;
    private float attackTimer;
    private float dashAttackTime = 0.5f;
    private float dashAttackTimer;
    private float jumpAttackTime = 0.3f;
    private float jumpAttackTimer;
    private float slideTime = 0.5f;
    private float slideTimer;
    private float h;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundDetector = GetComponent<GroundDetector>();
    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal"); //방향키 입력

        if(state != PlayerState.JumpAttack &&
           state != PlayerState.Attack &&
           state != PlayerState.DashAttack )
        {
            if (h < 0) direction = -1;
            else if (h > 0) direction = 1;
        } //방향전환

        if (Mathf.Abs(h) > moveInputOffset)
        {
            move.x = h;
            if (state == PlayerState.Idle )
                ChangePlayerState(PlayerState.Run);
        } //입력오프셋
        else move.x = 0;


        if (state != PlayerState.Jump &&
            state != PlayerState.Attack &&
            state != PlayerState.JumpAttack &&
            state != PlayerState.Dash &&
            !groundDetector.isDetected )
            ChangePlayerState(PlayerState.Fall);
        if (state == PlayerState.Dash && !groundDetector.isDetected) StartCoroutine(DashToFall());
        // 추락

        if (Input.GetButtonDown("Jump"))
        {
            if (groundDetector.isDetected &&
                state != PlayerState.Fall &&
                state != PlayerState.Dash &&
                state != PlayerState.Attack &&
                state != PlayerState.DashAttack &&
                state != PlayerState.Slide )
            {
                ChangePlayerState(PlayerState.Jump);
            }
        } // Jump
        if (Input.GetButtonDown("Dash"))
        {
            if (state != PlayerState.Jump && 
                state != PlayerState.Fall && 
                state != PlayerState.Attack && 
                state != PlayerState.DashAttack &&
                state != PlayerState.JumpAttack &&
                state != PlayerState.Slide )
                ChangePlayerState(PlayerState.Dash);            
        } //Dash
        if (Input.GetButtonDown("Attack"))
        {
            if (state != PlayerState.Dash &&
                state != PlayerState.Fall && 
                state != PlayerState.Jump &&
                state != PlayerState.DashAttack &&
                state != PlayerState.JumpAttack &&
                state != PlayerState.Slide )
                ChangePlayerState(PlayerState.Attack);
        } // Attack
        if (Input.GetButton("Crouch"))
        {
            if (state != PlayerState.Jump &&
                state != PlayerState.Fall &&
                state != PlayerState.Attack &&
                state != PlayerState.DashAttack &&
                state != PlayerState.Dash &&
                state != PlayerState.JumpAttack &&
                state != PlayerState.Slide)
                ChangePlayerState(PlayerState.Crouch);
        } //Crouch

        UpdatePlayerState();
    }
    private void FixedUpdate()
    {
        if (state != PlayerState.Dash &&
           state != PlayerState.Attack &&
           state != PlayerState.DashAttack &&
           state != PlayerState.Crouch &&
           state != PlayerState.Slide)
        {
            moveVector = new Vector2(move.x * moveSpeed, 0) * Time.fixedDeltaTime;
            rb.position += moveVector;
        }

        if(state == PlayerState.Dash && dashDir.x * h < 0)
        {
            moveVector = new Vector2(move.x * moveSpeed, 0) * Time.fixedDeltaTime;
            rb.position += moveVector;
        }
    }

    IEnumerator DashToFall()
    {
        yield return new WaitForSeconds(0.3f);
        if (!groundDetector.isDetected) ChangePlayerState(PlayerState.Fall);
        yield return null;
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
            case PlayerState.Crouch:
                crouchState = CrouchState.Idle;
                break;
            case PlayerState.Jump:
                jumpState = JumpState.Idle;
                break;
            case PlayerState.JumpAttack:
                jumpAttackState = JumpAttackState.Idle;
                break;
            case PlayerState.Fall:
                fallState = FallState.Idle;
                break;
            case PlayerState.Dash:
                dashState = DashState.Idle;
                break;
            case PlayerState.Attack:
                attackState = AttackState.Idle;
                break;
            case PlayerState.DashAttack:
                dashAttackState = DashAttack.Idle;
                break;
            case PlayerState.Slide:
                slideState = Slide.Idle;
                break;
            default:
                break;
        }

        state = newState; // 현재상태 바꿈
        
        switch (state) // 현재상태 하위머신 준비
        {
            case PlayerState.Idle:
                idleState = IdleState.Prepare;
                break;                
            case PlayerState.Run:
                runState = RunState.Prepare;
                break;
            case PlayerState.Crouch:
                crouchState = CrouchState.Prepare;
                break;
            case PlayerState.Jump:
                jumpState = JumpState.Prepare;
                break;
            case PlayerState.JumpAttack:
                jumpAttackState= JumpAttackState.Prepare;
                break;
            case PlayerState.Fall:
                fallState = FallState.Prepare;
                break;
            case PlayerState.Dash:
                dashState= DashState.Prepare;
                break;
            case PlayerState.Attack:
                attackState = AttackState.Prepare;
                break;
            case PlayerState.DashAttack:
                dashAttackState= DashAttack.Prepare;
                break;
            case PlayerState.Slide:
                slideState = Slide.Prepare;
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
            case PlayerState.Crouch:
                UpdateCrouchState();
                break;
            case PlayerState.Jump:
                UpdateJumpState();
                break;
            case PlayerState.JumpAttack:
                UpdateJumpAttack();
                break;
            case PlayerState.Fall:
                UpdateFallState();
                break;
            case PlayerState.Dash:
                UpdateDashState();
                break;
            case PlayerState.Attack:
                UpdateAttackState();
                break;
            case PlayerState.DashAttack:
                UpdateDashAttackState();
                break;
            case PlayerState.Slide:
                UpdateSlideState();
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
                idleState++;
                break;
            case IdleState.Prepare:
                animator.Play("Idle");
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
                jumpState++;
                break;
            case JumpState.Casting:
                if (!groundDetector.isDetected ) jumpState++;
                else if(jumpTimer < 0) ChangePlayerState(PlayerState.Idle);
                jumpTimer -= Time.deltaTime;
                break;
            case JumpState.OnAction:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.JumpAttack);
                if (rb.velocity.y <= 0)jumpState++;                
                break;
            case JumpState.Finish:
                ChangePlayerState(PlayerState.Fall);
                break;
            default:
                break;
        }
    }
    private void UpdateJumpAttack()
    {
        switch (jumpAttackState)
        {
            case JumpAttackState.Idle:
                break;
            case JumpAttackState.Prepare:
                animator.Play("JumpAttack");
                jumpAttackTimer = jumpAttackTime;
                jumpAttackState++;
                break;
            case JumpAttackState.Casting:
                if(jumpAttackTimer<0)jumpAttackState++;
                jumpAttackTimer -= Time.deltaTime;
                break;
            case JumpAttackState.OnAction:
                jumpAttackState++;
                break;
            case JumpAttackState.Finish:
                ChangePlayerState(PlayerState.Idle);
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
                rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y); //속도 초기화
                animator.Play("Fall");
                fallState++;
                break;
            case FallState.Casting:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.JumpAttack);
                if (groundDetector.isDetected ) fallState++;
                break;
            case FallState.OnAction:
                fallState++;
                break;
            case FallState.Finish:
                ChangePlayerState(PlayerState.Idle);
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
                break;
            case RunState.Prepare:
                animator.Play("Run");
                runState++;
                break;
            case RunState.Casting:
                runState++;
                break;
            case RunState.OnAction:
                if(move.x == 0) runState++;
                break;
            case RunState.Finish:
                ChangePlayerState(PlayerState.Idle); ;
                break;
            default:
                break;
        }
    }
    private void UpdateCrouchState()
    {
        switch (crouchState)
        {
            case CrouchState.Idle:
                break;
            case CrouchState.Prepare:
                animator.Play("Crouch");
                crouchState++;
                break;
            case CrouchState.Casting:
                crouchState++;
                break;
            case CrouchState.OnAction:
                if(Input.GetButtonUp("Crouch")) crouchState++;
                break;
            case CrouchState.Finish:
                ChangePlayerState(PlayerState.Idle);
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
                break;
            case DashState.Prepare:
                animator.Play("Dash");
                rb.velocity = new Vector2(0,rb.velocity.y);
                rb.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
                dashDir = transform.right;
                dashTimer = dashTime;
                dashState++;
                break;
            case DashState.Casting:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.DashAttack);
                if (Input.GetButton("Crouch")) ChangePlayerState(PlayerState.Slide);
                if (dashTimer <= 0) dashState++;
                dashTimer -= Time.deltaTime;
                break;
            case DashState.OnAction:
                dashState++;
                break;
            case DashState.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }
    }
    private void UpdateAttackState()
    {
        switch (attackState)
        {
            case AttackState.Idle:
                break;
            case AttackState.Prepare:
                attackTimer = attackTime;
                animator.Play("Attack");
                attackState++;
                break;
            case AttackState.Casting:
                attackState++;
                break;
            case AttackState.OnAction:
                if(attackTimer<0) attackState++;
                attackTimer -= Time.deltaTime;
                break;
            case AttackState.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }
    }
    private void UpdateDashAttackState()
    {
        switch (dashAttackState)
        {
            case DashAttack.Idle:
                break;
            case DashAttack.Prepare:
                dashAttackTimer = dashAttackTime;
                animator.Play("DashAttack");
                dashAttackState++;
                break;
            case DashAttack.Casting:
                if(dashAttackTimer<0) dashAttackState++;
                dashAttackTimer -= Time.deltaTime;
                break;
            case DashAttack.OnAction:
                dashAttackState++;
                break;
            case DashAttack.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }
    }
    private void UpdateSlideState()
    {
        switch (slideState)
        {
            case Slide.Idle:
                break;
            case Slide.Prepare:
                animator.Play("Slide");
                rb.AddForce(transform.right*((dashForce - Mathf.Abs(rb.velocity.x))*0.7f), ForceMode2D.Impulse);
                slideTimer = slideTime;
                slideState++;
                break;
            case Slide.Casting:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.DashAttack);
                if (slideTimer<0) slideState++;
                slideTimer -= Time.deltaTime;
                break;
            case Slide.OnAction:
                slideState++;
                break;
            case Slide.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }
    }
}
