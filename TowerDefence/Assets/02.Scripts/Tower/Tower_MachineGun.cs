using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_MachineGun : Tower
{
    public float damage;
    public float reloadTime;
    public Transform bullet;
    public float bulletSpeed;
    float tmpTimer;
    List<Transform> firePoints;
    bool _rest;


    private void Awake()
    {
        firePoints = new List<Transform>();
        Transform[] tmpTrs = GetComponentsInChildren<Transform>();
        foreach (Transform tr in tmpTrs)
        {
            if ("FirePoint" == tr.name)
            {
                firePoints.Add(tr);
            }
        }
    }

    protected override void Update()
    {
        base.Update();
        if (target != null) Fire();
    }

    void Fire()
    {
        
        float tmpTime = reloadTime / firePoints.Count;
        tmpTimer -= Time.deltaTime;
        foreach (Transform tr in firePoints)
        {
            if (tmpTimer < 0)
            {
                Transform tmpBullet = Instantiate(bullet, tr.position, tr.rotation);
                tmpBullet.parent = gameObject.transform;
                
                firePoints.RemoveAt(0);
                firePoints.Add(tr);

                tmpTimer = tmpTime;

                return;
            }
        }
    }

}
