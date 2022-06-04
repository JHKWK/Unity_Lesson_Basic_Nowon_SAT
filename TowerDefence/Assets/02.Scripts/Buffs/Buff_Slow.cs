using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Slow : Buff
{
    public float slowPercent;
    EnemyMovement enemyMovement;
    float enemySpeedOrigin;

    public override void OnActive(Enemy tartget)
    {
        base.OnActive(tartget);

        enemyMovement = tartget.GetComponent<EnemyMovement>();
        enemySpeedOrigin = enemyMovement.moveSpeed;

        float tmpSpeed = enemySpeedOrigin * (1f - slowPercent / 100f);
        if (enemyMovement.moveSpeed > tmpSpeed)
            enemyMovement.moveSpeed = tmpSpeed;
    }

    public override bool OnDuration(Enemy target)
    {
        return target == null ? false : true ;
    }
    public override void OnDeactive(Enemy target)
    {
        base.OnDeactive(target);

        if (target != null)
            enemyMovement.moveSpeed = enemySpeedOrigin;
    }
}
