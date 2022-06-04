using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Tower : MonoBehaviour
{
    public TowerInfo info;
    public LayerMask enemyLayer;
    public float detectRange;

    public Transform rotatePoint;
    public Transform target;

    protected Transform tr;
    protected virtual void Update()
    {
        tr= transform;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRange, enemyLayer);

        if (colliders.Length > 0)
        {
            colliders.OrderBy(x => (x.transform.position - WayPoints.instance.GetLastWayPoint().position).magnitude );
            target = colliders[0].transform;
            rotatePoint.LookAt(target);

        }
        else
        {
            target = null;
        }
    }

    protected virtual void FixedUpdate()
    {

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }

}
