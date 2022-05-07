using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SongDataInfo : MonoBehaviour
{
    string songTitle;
    int noteCount;
    int bpm;
    int gridSegment;
    float gridGap;
    int trackLength;
    Text text;
    Text previous;

    private void Awake()
    {
        text = this.GetComponent<Text>();
    }


    void Update()
    {
        StatusUpdate();
    }

    void StatusUpdate()
    {
        songTitle = ChartMaker.instance.clip.name;
        noteCount = ChartMaker.instance.notes.Count;
        bpm = ChartMaker.instance.bpm;
        gridSegment = ChartMaker.instance.gridSegment;
        gridGap = ChartMaker.instance.gridGap;
        trackLength = (int)ChartMaker.instance.trackLength;
        text.text = $"Title : {songTitle}" + "\n" +
                    $"Length : {trackLength} sec  /  NoteCount : {noteCount}" + "\n"+
                    $"BPM : {bpm}  /  Grid Segment : {gridSegment}" + "\n" +
                    $"Grid Gap : {gridGap} sec";
    }
}
