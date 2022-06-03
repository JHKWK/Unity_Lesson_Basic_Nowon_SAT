using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldZero : MonoBehaviour
{
    static WorldZero _instance;
    public static WorldZero instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<WorldZero>("Assets/WorldZero"));
            return _instance;
        }
    }
}
