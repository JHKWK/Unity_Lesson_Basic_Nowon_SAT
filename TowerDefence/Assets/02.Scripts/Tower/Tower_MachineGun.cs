using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_MachineGun : Tower
{
    public float damage;
    public float reloadTime;
    public Transform bullet;
    public float bulletSpeed;
    float reloadTimer;
    Transform firePoint;


    private void Awake()
    {
        Transform[] children = this.GetComponentsInChildren<Transform>();

        foreach (Transform tr in children)
        {
            if ("FirePoint" == tr.name)
            {
                firePoint = tr;
            }
        }
    }

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
        //target.GetComponentInParent<Enemy>().hp -= damage;
        Fire();
    }

    void Fire()
    {
        Transform tmpBullet = Instantiate(bullet,firePoint.position,firePoint.rotation);
        tmpBullet.parent = gameObject.transform;
    }
}
