using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isOn;
    [SerializeField] BoxCollider collider;
    [SerializeField] LayerMask GroundLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(isOn == false &&
            1 << other.gameObject.layer == GroundLayer)
        {
            isOn = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (1 << other.gameObject.layer == GroundLayer)
        {
            isOn = false;
        }
    }


}
