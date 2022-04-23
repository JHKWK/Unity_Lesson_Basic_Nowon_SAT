using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerUI ui;
    private PlayerController controller;

    public float hpMax = 100;
    private float _hp;

    public float hp
    {
        set
        {
            if (value > 0 && value < hpMax)
            {
                controller.ChangePlayerState(PlayerController.PlayerState.Hurt);

            }
            else if (value < 0)
            {
                controller.ChangePlayerState(PlayerController.PlayerState.Die);
                value = 0;
            }

            _hp = value;
            ui.SetHPBar(_hp / hpMax);
        }
        get
        {
            return _hp;
        }
    }

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        hp = hpMax;
    }

}
