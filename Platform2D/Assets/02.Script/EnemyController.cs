using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Hurt,
        Die
    }
    public enum MotionState
    {
        Idle,
        Prepare,
        Casting,
        OnAction,
        Finish
    }
    public enum AiState
    {
        Idle,
        DecideRandomBehavior,
        TakeARest,
        MoveLeft,
        MoveRight,
        FollowTarget,
        AttackTarget,
    }

    [Header("상태")]
    public EnemyState state;
    public MotionState idleState;
    public MotionState moveState;
    public MotionState attackState;
    public MotionState hurtState;
    public MotionState dieState;

    [Header("인공지능")]
    public AiState aiState;
    public bool aiAutoFollow;
    public float aiDetectRange;
    public bool aiAttackEnable;
    public float aiBehaviorTimeMin;
    public float aiBehaviorTimeMax;
    public float aiBehaviorTimer;
    public LayerMask aiTargetLayer;

    [Header("동작")]
    public Vector2 move;
    public Vector2 moveVector;
    public float moveSpeed = 2;
    int _direction;
    public int direction
    {
        set
        {
            if (value < 0)
            {
                _direction = -1;
                transform.eulerAngles = Vector3.zero;
            }
            else if (value > 0)
            {
                _direction = 1;
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }
        }

        get { return _direction; }
    }

    [Header("애니메이션")]
    public Animator animator;
    float animationTimer;
    float attackTime;
    float hurtTime;
    float dieTime;

    [Header("키네메틱스")]
    Rigidbody2D rb;
    BoxCollider2D col;
    GroundDetector groundDetector;

    public float damage = 10;
    public float attackKnockbackTime = 1;

    public void Knockback(Vector2 dir, float force, float time)
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(E_Knockback(dir, force, time));
    }
    IEnumerator E_Knockback(Vector2 dir, float force, float time)
    {
        float timer = time;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            rb.AddForce(dir * force, ForceMode2D.Force);
            rb.AddForce(Vector2.up * force * 1.5f, ForceMode2D.Force);
            yield return null;
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        groundDetector = GetComponent<GroundDetector>();
        col = GetComponent<BoxCollider2D>();

        attackTime = GetAnimationTime("Attack");
        hurtTime = GetAnimationTime("Hurt");
        dieTime = GetAnimationTime("Die");
    }

    private void Update()
    {
        UpdateEnemyState();
        UpdateAiState();

        if (move.x < 0) direction = -1;
        if (move.x > 0) direction = 1;

        if (Mathf.Abs(move.x) > 0)
        {
            if (state == EnemyState.Idle)
                ChangeEnemyState(EnemyState.Move);
        }
        else move.x = 0;

        

    }

    private void FixedUpdate()
    {
        moveVector = new Vector2(move.x * moveSpeed, 0) * Time.fixedDeltaTime;
        rb.position += moveVector;
    }


    private void OnDrawGizmos()
    {
        rb = GetComponent<Rigidbody2D>();
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(rb.position, aiDetectRange);

    }


    public void UpdateEnemyState()
    {
        switch (state)
        {
            case EnemyState.Idle:
                UpdateIdleState();
                break;
            case EnemyState.Move:
                UpdateMoveState();
                break;
            case EnemyState.Attack:
                UpdateAttackState();
                break;
            case EnemyState.Hurt:
                UpdateHurtState();
                break;
            case EnemyState.Die:
                UpdateDieState();
                break;
            default:
                break;
        }
    }
    public void ChangeEnemyState(EnemyState newState)
    {
        if (state == newState) return;

        switch (state)
        {
            case EnemyState.Idle:
                idleState = MotionState.Idle;
                break;
            case EnemyState.Move:
                moveState = MotionState.Idle;
                break;
            case EnemyState.Attack:
                attackState = MotionState.Idle;
                break;
            case EnemyState.Hurt:
                hurtState = MotionState.Idle;
                break;
            case EnemyState.Die:
                dieState = MotionState.Idle;
                break;
            default:
                break;
        }

        state = newState;

        switch (state)
        {
            case EnemyState.Idle:
                idleState = MotionState.Prepare;
                break;
            case EnemyState.Move:
                moveState = MotionState.Prepare;
                break;
            case EnemyState.Attack:
                attackState = MotionState.Prepare;
                break;
            case EnemyState.Hurt:
                hurtState = MotionState.Prepare;
                break;
            case EnemyState.Die:
                dieState = MotionState.Prepare;
                break;
            default:
                break;
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

    public void UpdateIdleState()
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
                idleState++;
                break;
            case MotionState.OnAction:
                break;
            case MotionState.Finish:
                break;
            default:
                break;
        }
    }

    public void UpdateAttackState()
    {
        switch (attackState)
        {
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Attack");
                attackState++;
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

    public void UpdateMoveState()
    {
        switch (moveState)
        {
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Move");
                idleState++;
                break;
            case MotionState.Casting:
                idleState++;
                break;
            case MotionState.OnAction:
                idleState++;
                break;
            case MotionState.Finish:
                break;
            default:
                break;
        }
    }

    public void UpdateHurtState()
    {
        switch (hurtState)
        {
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Hurt");
                animationTimer = hurtTime;
                
                hurtState++;
                break;
            case MotionState.Casting:
                animationTimer -= Time.deltaTime;
                hurtState++;
                break;
            case MotionState.OnAction:
                if (animationTimer < 0) hurtState++;
                else animationTimer -= Time.deltaTime;
                break;
            case MotionState.Finish:
                ChangeEnemyState(EnemyState.Idle);
                break;
            default:
                break;
        }
    }

    public void UpdateDieState()
    {
        switch (dieState)
        {
            case MotionState.Idle:
                break;
            case MotionState.Prepare:
                animator.Play("Die");                
                moveSpeed = 0;
                rb.velocity = Vector2.zero;
                col.enabled = false;
                animationTimer = dieTime;
                dieState++;
                break;
            case MotionState.Casting:
                dieTime -= Time.deltaTime;
                dieState++;
                break;
            case MotionState.OnAction:
                dieTime -= Time.deltaTime;
                if (dieTime < 0) dieState++;
                break;
            case MotionState.Finish:
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    void UpdateAiState()
    {
        if (aiAutoFollow)
        {
            if( Physics2D.OverlapCircle(rb.position, aiDetectRange, aiTargetLayer))
            {
                aiState = AiState.FollowTarget;
            } 
        }

        switch (aiState)
        {
            case AiState.Idle:
                aiState++;
                break;
            case AiState.DecideRandomBehavior:
                move.x = 0;
                aiBehaviorTimer =Random.Range(aiBehaviorTimeMin, aiBehaviorTimeMax);
                aiState = (AiState)Random.Range(2,5);
                break;
            case AiState.TakeARest:
                animator.Play("");
                if (aiBehaviorTimer < 0) aiState = AiState.DecideRandomBehavior;
                else aiBehaviorTimer -= Time.deltaTime;
                break;
            case AiState.MoveLeft:
                if (aiBehaviorTimer < 0) aiState = AiState.DecideRandomBehavior;
                else
                {
                    move.x = -1;
                    aiBehaviorTimer -= Time.deltaTime;
                }
                break;
            case AiState.MoveRight:
                if (aiBehaviorTimer < 0) aiState = AiState.DecideRandomBehavior;
                else
                {
                    move.x = 1;
                    aiBehaviorTimer -= Time.deltaTime;
                }
                break;
            case AiState.FollowTarget:
                Collider2D target = Physics2D.OverlapCircle(rb.position, aiDetectRange, aiTargetLayer);
                if (target == null)
                    aiState = AiState.DecideRandomBehavior;

                else if (target.transform.position.x > rb.position.x + col.size.x) move.x = 1;
                else if (target.transform.position.x < rb.position.x) move.x = -1;

                break;
            case AiState.AttackTarget:
                break;
            default:
                break;
        }
    }
}
