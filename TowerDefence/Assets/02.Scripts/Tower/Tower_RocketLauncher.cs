using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower_RocketLauncher : Tower
{
    public GameObject rocketPrefab;
    public int damage;
    public int reloadTime;
    float _reloadTimer;

    [SerializeField] Transform[] firePoints;

    protected override void Update()
    {
        base.Update();
        //¿Á¿Â¿¸
        if (_reloadTimer < 0)
        {
            if (target != null)
            {
                Attack();
                _reloadTimer = reloadTime;
            }
        }
        else _reloadTimer -= Time.deltaTime;

        void Attack()
        {
            foreach (Transform firePoint in firePoints)
            {
                GameObject rocket = Instantiate(rocketPrefab, firePoint.position, Quaternion.identity);
                Vector3 dir = (target.transform.position - rocket.transform.position).normalized;
                rocket.GetComponent<Rocket>().Setup(dir, damage, target);
            }
        }
    }
}
