using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBPM : MonoBehaviour
{
    public static InputBPM instance;

    public int min;
    public int max;
    public int bpm;
    public Text inputBpm;

    private void Awake()
    {
        instance = this;
    }
    public void Input()
    {
        bpm = int.Parse(inputBpm.text);
        bpm = Mathf.Clamp(bpm, min, max);
        this.GetComponent<InputField>().text = bpm.ToString();
        ChartMaker.instance.ChangeBpm(bpm);
    }
}
