using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject destroyEffect;
    public Slider hpSlider;

    public int damage;
    public int score;

    public int hpMax;
    private int _hp;

    private void Awake()
    {
        hp = hpMax;
    }
    public int hp
    {
        set
        {
            if(value > 0) _hp = value;

            else
            {
                _hp = 0;
                Player.Instance.score += score;
                DoDestroyEffect();
                Destroy(gameObject);
            } 

            hpSlider.value = (float)_hp / hpMax;
        }
        get { return _hp; }
    }



    public void DoDestroyEffect()
    {
        GameObject go = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(go, 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.Instance.hp -= damage;
            Player.Instance.score += score;
            DoDestroyEffect();
            Destroy(gameObject);
        }
    }
}
