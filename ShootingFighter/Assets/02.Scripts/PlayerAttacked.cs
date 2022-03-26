using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacked : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;
        if (go == null) return;

        if (go.layer == LayerMask.NameToLayer("Enemy"))
        {   
            go.GetComponent<Enemy>().DoDestroyEffect();
            Destroy(go);
        }
    }



}
