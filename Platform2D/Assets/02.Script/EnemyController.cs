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

    [Header("상태")]
    public EnemyState state;
    public MotionState idleState;
    public MotionState moveState;
    public MotionState attackState;
    public MotionState hurtState;
    public MotionState dieState;
    
    [Header("애니메이션")]
    public Animator animator;
    float animationTimer;
    float attackTime;
    float hurtTime;
    float dieTime;

    [Header("키네메틱스")]
    Rigidbody2D rb;
    Vector2 move;
    GroundDetector groundDetector;

    public void Knockback(Vector2 dir,float force, float time)
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(E_Knockback(dir, force, time));
    }
    IEnumerator E_Knockback(Vector2 dir,float force, float time)
    {
        float timer = time;
        while (timer > 0)
        {
            timer-=Time.deltaTime;
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

        attackTime = GetAnimationTime("Attack");
        hurtTime = GetAnimationTime("Hurt");
        dieTime = GetAnimationTime("Die");
    }

    private void Update()
    {
        UpdateEnemyState();
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
    void ChangeEnemyState(EnemyState newState)
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
                moveState= MotionState.Prepare;
                break;
            case EnemyState.Attack:
                attackState= MotionState.Prepare;
                break;
            case EnemyState.Hurt:
                hurtState= MotionState.Prepare;
                break;
            case EnemyState.Die:
                dieState= MotionState.Prepare;
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
                idleState++;
                break;
            case MotionState.Casting:
                animationTimer -= Time.deltaTime;
                idleState++;
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
                animationTimer = dieTime;
                dieState++;
                break;
            case MotionState.Casting:
                dieTime -= Time.deltaTime;
                idleState++;
                break;
            case MotionState.OnAction:
                dieTime -= Time.deltaTime;
                if(dieTime<0) idleState++;
                break;
            case MotionState.Finish:
                ChangeEnemyState(EnemyState.Idle);
                break;
            default:
                break;
        }
    }
}
