using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data_SkinSetInfo", menuName = "ScriptableObjects/SkinSetInfo")]
public class SkinInfo : ScriptableObject
{
    [Header ("SkinSetTheme")]
    [SerializeField] string skinSetName;

    [Header("���׸���")]
    public Material material_opened;
    public Material material_unOpen;
    public Material material_press;
    public Material material_Flag;
    public Material material_Mine;

    [Header("ȿ��")]
    public ParticleSystem EffectExploding;
    public ParticleSystem EffectPop;

    [Header("number �÷���")]
    public Color color1;
    public Color color2;
    public Color color3;
    public Color color4;
    public Color color5;
    public Color color6;
    public Color color7;
    public Color color8;
    [Header("number ��Ʈ")]
    public Font font;

    [Header("UI-Life skin")]
    public GameObject lifePrefab;

}
