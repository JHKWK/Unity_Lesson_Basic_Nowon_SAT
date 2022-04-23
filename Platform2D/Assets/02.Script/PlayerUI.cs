using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    public void SetHPBar(float value) =>
        hpBar.value = value;
}
