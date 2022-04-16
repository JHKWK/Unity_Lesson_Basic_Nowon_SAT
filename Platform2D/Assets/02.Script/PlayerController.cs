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
    public enum MotionState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
   
    public PlayerState state;
    public MotionState idleState;
    public MotionState runState;
    public MotionState crouchState;
    public MotionState jumpState;
    public MotionState jumpAttackState;
    public MotionState fallState;
    public MotionState dashState;
    public MotionState attackState;
    public MotionState dashAttackState;
    public MotionState slideState;

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
            else if (value > 0)
            {
                _direction = 1;
                transform.eulerAngles = Vector3.zero;
            }
        }

        get { return _direction; }
    }

    public LayerMask enemyLayer;

    public Vector2 attackBoxCastCenter;
    public Vector2 attackBoxCastSize;        

    private Rigidbody2D rb;
    private int _direction; // +1 :right , -1 = left
    private Vector2 dashDir;
    private float moveInputOffset = 0.1f;
    private Vector2 move;
    private Animator animator;
    private GroundDetector groundDetector;


    public float attackKnockbackForce;
    public float attackKnockbackTime;

    private float jumpCastingTime = 0.1f;
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
    bool isEnemyHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundDetector = GetComponent<GroundDetector>();

        attackTime = GetAnimationTime("Attack") * 0.5f;
        dashAttackTime = GetAnimationTime("DashAttack") * 0.5f;
        jumpAttackTime = GetAnimationTime("JumpAttack");
        slideTime = GetAnimationTime("Slide");
    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal"); //방향키 입력

        if(state != PlayerState.JumpAttack &&
           state != PlayerState.Attack &&
           state != PlayerState.DashAttack )
        {
            if (h < 0) direction = -1;
            if (h > 0) direction = 1;
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
             state != PlayerState.DashAttack &&
            state != PlayerState.Dash &&
            !groundDetector.isDetected )
            ChangePlayerState(PlayerState.Fall);
        if (state == PlayerState.Dash && state == PlayerState.DashAttack && !groundDetector.isDetected) StartCoroutine(DashToFall());
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
    float GetAnimationTime(string name)
    {
        float time = 0f;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == name) time = ac.animationClips[i].length;
        }
        return time;
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
                idleState = MotionState.Idle;
                break;
            case PlayerState.Run:
                runState = MotionState.Idle;
                break;
            case PlayerState.Crouch:
                crouchState = MotionState.Idle;
                break;
            case PlayerState.Jump:
                jumpState = MotionState.Idle;
                break;
            case PlayerState.JumpAttack:
                jumpAttackState = MotionState.Idle;
                break;
            case PlayerState.Fall:
                fallState = MotionState.Idle;
                break;
            case PlayerState.Dash:
                dashState = MotionState.Idle;
                break;
            case PlayerState.Attack:
                attackState = MotionState.Idle;
                break;
            case PlayerState.DashAttack:
                dashAttackState = MotionState.Idle;
                break;
            case PlayerState.Slide:
                slideState = MotionState.Idle;
                break;
            default:
                break;
        }

        state = newState; // 현재상태 업데이트
        
        switch (state) // 현재상태 하위머신 준비
        {
            case PlayerState.Idle:
                idleState = MotionState.Prepare;
                break;                
            case PlayerState.Run:
                runState = MotionState.Prepare;
                break;
            case PlayerState.Crouch:
                crouchState = MotionState.Prepare;
                break;
            case PlayerState.Jump:
                jumpState = MotionState.Prepare;
                break;
            case PlayerState.JumpAttack:
                jumpAttackState= MotionState.Prepare;
                break;
            case PlayerState.Fall:
                fallState = MotionState.Prepare;
                break;
            case PlayerState.Dash:
                dashState= MotionState.Prepare;
                break;
            case PlayerState.Attack:
                attackState = MotionState.Prepare;
                break;
            case PlayerState.DashAttack:
                dashAttackState= MotionState.Prepare;
                break;
            case PlayerState.Slide:
                slideState = MotionState.Prepare;
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
            case MotionState.Idle:
                idleState++;
                break;
            case MotionState.Prepare:
                animator.Play("Idle");
                idleState++;
                break;
            case MotionState.Casting:
                break;
            case MotionState.OnAction:
                break;
            case MotionState.Finish:
                break;
            default:
                break;
        }
    }
    private void UpdateJumpState()
    {
        switch (jumpState)
        {
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Jump");
                rb.velocity = new Vector2(rb.velocity.x, 0); //속도 초기화
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                jumpTimer = jumpCastingTime;
                jumpState++;
                break;
            case MotionState.Casting:
                if (!groundDetector.isDetected ) jumpState++;
                else if(jumpTimer < 0) ChangePlayerState(PlayerState.Idle);
                jumpTimer -= Time.deltaTime;
                break;
            case MotionState.OnAction:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.JumpAttack);
                if (rb.velocity.y <= 0)jumpState++;                
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                rb.velocity = new Vector2(rb.velocity.x * 0.3f, rb.velocity.y); //속도 초기화
                animator.Play("Fall");
                fallState++;
                break;
            case MotionState.Casting:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.JumpAttack);
                if (groundDetector.isDetected ) fallState++;
                break;
            case MotionState.OnAction:
                fallState++;
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Run");
                runState++;
                break;
            case MotionState.Casting:
                runState++;
                break;
            case MotionState.OnAction:
                if(move.x == 0) runState++;
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Crouch");
                crouchState++;
                break;
            case MotionState.Casting:
                crouchState++;
                break;
            case MotionState.OnAction:
                if(Input.GetButtonUp("Crouch")) crouchState++;
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Dash");
                rb.velocity = new Vector2(0,rb.velocity.y);
                rb.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
                dashDir = transform.right;
                dashTimer = dashTime;
                dashState++;
                break;
            case MotionState.Casting:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.DashAttack);
                if (Input.GetButton("Crouch")) ChangePlayerState(PlayerState.Slide);
                if (dashTimer <= 0) dashState++;
                dashTimer -= Time.deltaTime;
                break;
            case MotionState.OnAction:
                dashState++;
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Slide");
                rb.AddForce(transform.right * ((dashForce - Mathf.Abs(rb.velocity.x)) * 0.7f), ForceMode2D.Impulse);
                slideTimer = slideTime;
                slideState++;
                break;
            case MotionState.Casting:
                if (Input.GetButtonDown("Attack")) ChangePlayerState(PlayerState.DashAttack);
                if (slideTimer < 0) slideState++;
                slideTimer -= Time.deltaTime;
                break;
            case MotionState.OnAction:
                slideState++;
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                attackTimer = attackTime;
                animator.Play("Attack");
                attackState++;
                break;
            case MotionState.Casting:
                
                if(attackTimer < attackTime/2)
                {
                    if (!isEnemyHit)
                    { 
                        Vector2 tmpCenter = new Vector2(attackBoxCastCenter.x * direction, attackBoxCastCenter.y) + rb.position;
                        RaycastHit2D[] hits = Physics2D.BoxCastAll(tmpCenter, attackBoxCastSize, 0, Vector2.zero, 0, enemyLayer);
                        foreach (var hit in hits)
                        {
                            if (hit.collider != null) hit.collider.GetComponent<EnemyController>().Knockback(new Vector2(direction, 0), attackKnockbackForce , attackKnockbackTime);
                            Debug.Log("Hit");
                        }
                        isEnemyHit = true;
                        attackState++;
                    }                    
                }
                else attackTimer -= Time.deltaTime;
                break;
            case MotionState.OnAction:
                isEnemyHit = false;
                if (attackTimer<0) attackState++;
                else attackTimer -= Time.deltaTime;
                break;
            case MotionState.Finish:
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
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                dashAttackTimer = dashAttackTime;
                animator.Play("DashAttack");
                dashAttackState++;
                break;
            case MotionState.Casting:
                dashAttackTimer -= Time.deltaTime;

                if (!isEnemyHit)
                {
                    Vector2 tmpCenter = new Vector2(attackBoxCastCenter.x * direction, attackBoxCastCenter.y) + rb.position;
                    Vector2 dashAttackBoxCastSize = new Vector2(attackBoxCastSize.x * 1.5f, attackBoxCastSize.y);
                    RaycastHit2D[] hits = Physics2D.BoxCastAll(tmpCenter, dashAttackBoxCastSize, 0, Vector2.zero, 1, enemyLayer);
                    foreach (var hit in hits)
                    {
                        if (hit.collider != null) hit.collider.GetComponent<EnemyController>().Knockback(new Vector2(direction, 0), attackKnockbackForce*1.5f, attackKnockbackTime);
                        Debug.Log("Hit");
                    }
                    isEnemyHit = true;
                }
                else if (dashAttackTimer<0) dashAttackState++;
                break;
            case MotionState.OnAction:
                isEnemyHit = false;
                dashAttackState++;
                break;
            case MotionState.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }
    }
    private void UpdateJumpAttack()
    {
        switch (jumpAttackState)
        {
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("JumpAttack");
                jumpAttackTimer = jumpAttackTime;
                jumpAttackState++;
                break;
            case MotionState.Casting:

                jumpAttackTimer -= Time.deltaTime;

                Vector2 tmpCenter = new Vector2(attackBoxCastCenter.x * direction, attackBoxCastCenter.y) + rb.position;
                Vector2 dashAttackBoxCastSize = new Vector2(attackBoxCastSize.x * 1.5f, attackBoxCastSize.y);
                RaycastHit2D[] hits = Physics2D.BoxCastAll(tmpCenter, dashAttackBoxCastSize, 0, Vector2.zero, 1, enemyLayer);

                if (!isEnemyHit)
                {
                    foreach (var hit in hits)
                    {
                        if (hit.collider != null) hit.collider.GetComponent<EnemyController>().Knockback(new Vector2(direction, 0), attackKnockbackForce * 1.5f, attackKnockbackTime);
                        Debug.Log("Hit");
                    }
                    isEnemyHit = true;
                }

                else if (jumpAttackTimer < 0) jumpAttackState++;

                break;
            case MotionState.OnAction:
                isEnemyHit = false;
                jumpAttackState++;
                break;
            case MotionState.Finish:
                ChangePlayerState(PlayerState.Idle);
                break;
            default:
                break;
        }
    }
    private void OnDrawGizmos()
    {
        rb = GetComponent<Rigidbody2D>();
        Gizmos.color = Color.red;
        Vector2 tmpCenter = new Vector2(attackBoxCastCenter.x * direction, attackBoxCastCenter.y) + rb.position;
        Gizmos.DrawWireCube(tmpCenter, attackBoxCastSize);
    }
}
