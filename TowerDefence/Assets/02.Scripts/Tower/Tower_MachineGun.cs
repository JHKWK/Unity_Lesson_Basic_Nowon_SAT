using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_MachineGun : Tower
{
    public float damage;
    public float reloadTime;
    float reloadTimer;

    protected override void Update()
    {
        base.Update();

        //������
        if(reloadTimer < 0)
        {
            if (target != null)
            {
                Attack();
                reloadTimer = reloadTime;
            }
        }

        else
        {
            reloadTimer -= Time.deltaTime;
        }
    }

    void Attack()
    {
        //target�� ü���� damage��ŭ ��ŭ ����
        target.GetComponentInParent<Enemy>().hp -= damage;
    }

}
