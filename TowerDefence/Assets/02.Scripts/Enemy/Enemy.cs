using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    public float hpMax;

    float _hp;
    public float hp
    {
        get 
        {
            return _hp; 
        }

        set 
        {
            _hp = value;

            if (_hp < 0)
            {
                Destroy(gameObject);
            }

            hpBar.value = _hp / hpMax;
        }
    }
    


    private void Awake()
    {
        hp = hpMax;
    }

}
