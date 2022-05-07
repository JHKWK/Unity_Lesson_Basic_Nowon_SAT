using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Video;
using UnityEngine.UI;
/// <summary>
/// 105 bpm
/// 1/4박 초당 1.75개
/// 분당 노트개수
/// 1/4  = 분당 105 노트, 초당 1.75 노트   노트간격  0.5714285714285714 초
/// 420노트
/// bpm * 4 = 분당노트수
/// 60/bpm*4 =노트간 간격 0.1428571428571429 초 ()
/// 
/// 1분에 105개
/// 2분에 210개
/// 
/// 105 bpm은 분당 105
/// 105/60 초당 1.75개
/// 
/// 
/// 
/// 
/// 1/8  = 분당 210 노트, 초당 3.5  노트   노트간격  0.2857142857142857 초
/// 1/16 = 분당 420 노트, 초당 7    노트   노트간격  0.1428571428571429 초
/// 1/32 = 분당 840 노트, 초당 14   노트   노트간격  0.0714285714285714 초
/// 
/// </summary>
public class ChartMaker : MonoBehaviour
{
    public static ChartMaker instance;

    KeyCode[] keyCodes = { KeyCode.S,
                           KeyCode.D,
                           KeyCode.F,
                           KeyCode.Space,
                           KeyCode.J,
                           KeyCode.K,
                           KeyCode.L };
    [SerializeField] private VideoPlayer vp;
    [SerializeField] float gridThickness = 0.02f;
    [SerializeField] float noteHeight = 0.15f;

    int _bpm;
    int _gridSegment;
    float _noteSpeed;
    bool snapOn;
    bool recordOn;

    public float trackLength;
    public float gridGap;
    public float ChangeSpeed 
    {
        set
        {
            _noteSpeed = value;
            tr.localScale = new Vector3(1, _noteSpeed, 1);
            InstanceRescale();
        }
        get
        {
            return _noteSpeed;
        } 
    }
    public int bpm
    {
        get
        {
            return _bpm;
        }
        set
        {
            _bpm = value;
            gridGap = (1 / ((float)_bpm / 60)) / _gridSegment;
        }
    }
    public int gridSegment
    {
        get
        {
            return _gridSegment;
        }
        set
        {
            _gridSegment = value;
            gridGap = (1 / ((float)_bpm / 60)) / _gridSegment;
        }

    }

    public VideoClip clip;
    public Slider slider;
    public GameObject trackPrefab;
    public GameObject gridPrefab;
    public GameObject notesPrefab;
    public GameObject notesPrefab2;
    public GameObject redLine;

    Transform tr;
    Transform notesGroup;
    Transform gridsGroup;
    GameObject track;
    List<GameObject> grids;
    public List<GameObject> notes;
    public SongData songData;
    public GameObject inputBPM;

    private void Awake()
    {
        instance = this;
        tr = transform;

        _noteSpeed = 1;
        bpm = 60;
        gridSegment = 2;

        notesGroup = transform.Find("NotesGroup");
        gridsGroup = transform.Find("GridsGroup");
        grids = new List<GameObject>();
        notes = new List<GameObject>();

        slider.maxValue = (float)vp.length;
    }

    private void Start()
    {
        //LoadSongData();
        //StartCoroutine(SetUI());
        StartCoroutine(E_SetUp());
    }

    IEnumerator E_SetUp()
    {
        yield return new WaitUntil(() => vp.clip != null);
        yield return new WaitUntil(() => songData != null);

        LoadSongData();
        InputBPM.instance.GetComponent<InputField>().text = bpm.ToString(); //  set UI
    }

    private void FixedUpdate()
    {
        ChartScroll();
        VideoSliderScroll();
    }
    private void Update()
    {
        if(snapOn)
            GridSnap();
        if(recordOn)
            RecordNote();

        if (notes.Count != notesGroup.childCount)
            ResetNotesList();
    }
    public void LoadSongData()
    {
        CleanInstance();

        TextAsset songDataText = Resources.Load<TextAsset>($"SongDatas/{clip.name}");
        songData = JsonUtility.FromJson<SongData>(songDataText.ToString());
        bpm = songData.bpm;
        gridSegment = songData.segment;

        MakeTrack();
        MakeGrid();
        MakeNoteFromSongData();
        InstanceRescale();
    }
    public void ExportSongData()
    {
        NoteToSongData();
        string dir = EditorUtility.SaveFilePanel("저장할곳을입력하세요", "SongDatas/", songData.videoName, "json");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(songData));
    }
    public void NewChart()
    {
        CleanInstance();
        MakeTrack();
        MakeGrid();
        InstanceRescale();
    }
    public void VideoTimeChange()
    {
        if (!vp.isPlaying) vp.time = slider.value;
    }
    public void SnapOnOff()
    {
        if (snapOn) snapOn = false;
        else snapOn = true;
    }
    public void RecordOnOff()
    {
        if (recordOn) recordOn = false;
        else recordOn = true;
    }
    public void ChangeGridGap(int a)
    {
        gridSegment = a;
        foreach (GameObject item in grids) Destroy(item);
        grids = new List<GameObject>();
        MakeGrid();
    }
    public void ChangeBpm(int a)
    {
        bpm = a;
        foreach (GameObject item in grids) Destroy(item);
        grids = new List<GameObject>();
        MakeGrid();
    }
    public void ResetNotesList()
    {
        notes = new List<GameObject>();
        
        int tmp = notesGroup.childCount;
        for (int i = 0; i < tmp; i++)
        {
            notes.Add(notesGroup.GetChild(i).gameObject);
        }
    }
    void MakeTrack()
    {
        //track rail 생성
        trackLength = (float)vp.length;
        track = Instantiate(trackPrefab, Vector3.zero, Quaternion.identity);
        track.transform.SetParent(this.transform, true);
        track.transform.localScale = new Vector3(7, trackLength,0);
        track.transform.localPosition = new Vector3(0, +trackLength/2f, 1); //위치 조정
    }
    void MakeGrid()
    {
        bool isGridMaked = false;
        int n = 0;
        while (!isGridMaked)
        {
            grids.Add(Instantiate(gridPrefab, new Vector3(3, n * gridGap, 0.5f), Quaternion.identity));
            n++;
            if (n * gridGap > trackLength) isGridMaked = true;
        }
        foreach (GameObject item in grids)
        {
            item.transform.SetParent(gridsGroup, false);
            item.transform.localScale = new Vector3(7, gridThickness / _noteSpeed, 1);
        }
    }
    void CleanInstance()
    {
        foreach (GameObject item in grids) Destroy(item);
        foreach (GameObject item in notes) Destroy(item);
        Destroy(track);
        grids = new List<GameObject>();
        notes = new List<GameObject>();
    }
    void NoteToSongData()
    {
        songData = new SongData();
        songData.bpm = bpm;
        songData.videoName = vp.clip.name;
        songData.segment = gridSegment;
        KeyCode key = new KeyCode();
        //노트 생성
        foreach (var item in notes)
        {
            if(item==null) continue;
            else
            {
                NoteData noteData = new NoteData();
                switch (item.transform.localPosition.x)
                {
                    case 0:
                        key = KeyCode.S;
                        break;
                    case 1:
                        key = KeyCode.D;
                        break;
                    case 2:
                        key = KeyCode.F;
                        break;
                    case 3:
                        key = KeyCode.Space;
                        break;
                    case 4:
                        key = KeyCode.J;
                        break;
                    case 5:
                        key = KeyCode.K;
                        break;
                    case 6:
                        key = KeyCode.L;
                        break;
                }
                noteData.keyCode = key;
                noteData.time = item.transform.localPosition.y;
                songData.notes.Add(noteData);

            }
        }
    }
    void MakeNoteFromSongData()
    {
        foreach (var item in songData.notes)
        {
            int keyCode =0;
            switch (item.keyCode)
            {
                case KeyCode.S: keyCode = 0; break;
                case KeyCode.D: keyCode = 1; break;
                case KeyCode.F: keyCode = 2; break;
                case KeyCode.Space: keyCode = 3; break;
                case KeyCode.J: keyCode = 4; break;
                case KeyCode.K: keyCode = 5; break;
                case KeyCode.L: keyCode = 6; break;
            }

            if (keyCode == 0 || keyCode == 2 || keyCode == 4 || keyCode == 6) notes.Add(Instantiate(notesPrefab, new Vector3(keyCode, item.time, 0), Quaternion.identity));
            else notes.Add(Instantiate(notesPrefab2, new Vector3(keyCode, item.time, 0), Quaternion.identity));
        }
        //오브젝트 이동
        foreach (var item in notes) item.transform.SetParent(transform.Find("NotesGroup"), false);
    }
    void RecordNote()
    {
        foreach (KeyCode keyCode in keyCodes)
        {
            if (Input.GetKeyDown(keyCode))
            {
                switch (keyCode)
                {
                    case KeyCode.S:
                        {
                            notes.Add(Instantiate(notesPrefab, new Vector3(0, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                    case KeyCode.D:
                        {
                            notes.Add(Instantiate(notesPrefab2, new Vector3(1, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                    case KeyCode.F:
                        {
                            notes.Add(Instantiate(notesPrefab, new Vector3(2, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                    case KeyCode.Space:
                        {
                            notes.Add(Instantiate(notesPrefab2, new Vector3(3, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                    case KeyCode.J:
                        {
                            notes.Add(Instantiate(notesPrefab, new Vector3(4, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                    case KeyCode.K:
                        {
                            notes.Add(Instantiate(notesPrefab2, new Vector3(5, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                    case KeyCode.L:
                        {
                            notes.Add(Instantiate(notesPrefab, new Vector3(6, (float)vp.time, 0), Quaternion.identity));
                            break;
                        }
                }
                foreach (var item in notes)
                {
                    if (item != null)
                    {
                        item.transform.SetParent(transform.Find("NotesGroup"), false);
                        item.transform.localScale = new Vector3(1, noteHeight / _noteSpeed, 1);
                    }

                }
            }                
        }
    }
    void GridSnap()
    {
        foreach (var item in notes)
        {
            if (item != null)
            {
                if(item.transform.localPosition.y % gridGap != 0)
                {
                    if (item.transform.localPosition.y % gridGap < gridGap / 2f)
                        item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                                                   item.transform.localPosition.y - (item.transform.localPosition.y % gridGap), 0);
                    if (item.transform.localPosition.y % gridGap > gridGap / 2f)
                        item.transform.localPosition = new Vector3(item.transform.localPosition.x,
                                                                   item.transform.localPosition.y - (item.transform.localPosition.y % gridGap) + gridGap, 0);
                }
                if(item.transform.localPosition.x % 1 != 0 )
                {
                    if (item.transform.localPosition.x % 1 < 1/2f)
                        item.transform.localPosition = new Vector3(item.transform.localPosition.x - (item.transform.localPosition.x % 1),
                                                                    item.transform.localPosition.y, 0);
                    if (item.transform.localPosition.x % 1 > 1/2f)
                        item.transform.localPosition = new Vector3(item.transform.localPosition.x - (item.transform.localPosition.x % 1)+1,
                                                                    item.transform.localPosition.y, 0);
                }
            }
        }
    }
    void ChartScroll()
    {
        tr.localPosition = new Vector3(redLine.transform.localPosition.x,
                                       (float)((double)redLine.transform.localPosition.y - vp.time * _noteSpeed), 0);
    }
    void InstanceRescale()
    {
        foreach(var item in notes)
        {
            if (item != null) item.transform.localScale = new Vector3(1, noteHeight / _noteSpeed, 1);
        }
        foreach (var item in grids)
        {
            if (item != null) item.transform.localScale = new Vector3(7, gridThickness / _noteSpeed, 1);
        }
    }
    void VideoSliderScroll()
    {
        if (vp.isPlaying) slider.value = (float)vp.time;
    }

}
