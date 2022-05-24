using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TowerType
{
    MachinGune,
    RocketLauncher,
    Laser,
}

[CreateAssetMenu(fileName = "Data_Tower", menuName = "ScriptableObjects/Create TowerInfo")]
public class TowerInfo : ScriptableObject
{
    public TowerType type;
    public int upgradeLevel;
    public int buildPrice;
    public int sellPrice;
    public Sprite icon;
}
