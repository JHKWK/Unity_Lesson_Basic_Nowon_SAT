using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explodingPrefab;
    public Slider hpBar;
    public float hpMax;
    Vector3 offset = new Vector3(0, 0.5f, 0);

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
                LevelManager.instance.killedEnemy++;
                Instantiate(explodingPrefab, transform.position + offset, Quaternion.Euler(-90,0,0));
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
