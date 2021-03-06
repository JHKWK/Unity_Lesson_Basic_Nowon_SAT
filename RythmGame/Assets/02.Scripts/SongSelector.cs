using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SongSelector : MonoBehaviour 
{
    public static SongSelector instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public VideoClip clip;
    public SongData songData;
    public bool isPlayable
    {
        get
        {
            return (clip != null) && (songData != null) ? false : true;
        }
    }

    public void LoadSongData(string clipName)
    {
        clip = Resources.Load<VideoClip>($"VideoClips/{clipName}"); // 비디오 클립 로드

        TextAsset songDataText = Resources.Load<TextAsset>($"SongDatas/{clipName}");
        songData = JsonUtility.FromJson<SongData>(songDataText.ToString());  // json 데이터 Deserialize 
    }
}
