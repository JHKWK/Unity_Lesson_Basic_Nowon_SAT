using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player intance;

    private void Awake()
    {
        intance = this;
    }
    public void Move(Vector3 target)
    {
        transform.position = target;
    }

}