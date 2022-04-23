using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyUI ui;
    private EnemyController controller;

    public float hpMax = 100;
    private float _hp;
    public float damage = 10;
    public float knockBackForce = 2;
    public float knockBackTime = 1;
    public float attackRate = 0.5f;
    float attackTimer;

    public float hp
    {
        set
        {
            if (value > 0 && value < hpMax)
            {
                controller.ChangeEnemyState(EnemyController.EnemyState.Hurt);

            }
            else if (value <0 )
            {
                controller.ChangeEnemyState(EnemyController.EnemyState.Die);
                value = 0;
            }

                _hp = value;
                ui.SetHPBar(_hp/hpMax);            
        }
        get 
        {
            return _hp; 
        }
    }

    private void Awake()
    {
        controller = GetComponent<EnemyController>();
        hp = hpMax;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (attackTimer < 0)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                //플레이어 넉백 // 체력 감소
                //direction
                Vector2 direction = new Vector2(controller.direction, 0);
                collision.GetComponent<Player>().hp -= damage;
                collision.GetComponent<PlayerController>().Knockback(direction, knockBackForce, knockBackTime);
                collision.GetComponent<PlayerController>().direction = (int)direction.x * -1;
                attackTimer = attackRate;
            }
        }
        else attackTimer -= Time.deltaTime;

    }
}
