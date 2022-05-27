using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEffect : MonoBehaviour
{
    public float killTime;

    private void Update()
    {
        killTime -= Time.deltaTime;
        if (killTime < 0)
            Destroy(gameObject);
    }
}
