using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public static NoteManager instance;
    public static float noteSpeedScale = 2.5f;
    [SerializeField] private Transform spawnersParent;
    [SerializeField] private Transform hitterParent;

    public float noteFallingDistance
    {
        get
        {
            return spawnersParent.position.y - hitterParent.position.y ;
        } 
    }
    public float noteFallingTime
    {
        get
        {
            return noteFallingDistance / noteSpeedScale;
        }
    }

    private Dictionary<KeyCode,NoteSpawner> spawners = new Dictionary<KeyCode, NoteSpawner>();
    private Queue<NoteData> noteQueue = new Queue<NoteData>();
    //==================================================================================
    //********************************* Public Methods *********************************
    //==================================================================================
    
    public void startSpawn()
    {
        Debug.Log("Check");
        if (noteQueue.Count > 0)
            StartCoroutine(E_Spawning());
    }
    IEnumerator E_Spawning()
    {
        float startTimeMark = Time.time;        
        while(noteQueue.Count>0)
        {
            for(int i = 0; i < noteQueue.Count; i++)
            {
                if ( noteQueue.Peek().time < (Time.time - startTimeMark) / noteSpeedScale )
                {
                    NoteData note = noteQueue.Dequeue();
                    if( note.speed > 0)
                        spawners[note.keyCode].SpawnNote().speed = note.speed;
                    else
                        spawners[note.keyCode].SpawnNote();
                }
                else
                    break;
            }
            yield return null;
        }
    }

    //==================================================================================
    //********************************* Private Methods *********************************
    //==================================================================================
    private void Awake()
    {
        instance = this;
        
        NoteSpawner[] tmpSpawners = spawnersParent.GetComponentsInChildren<NoteSpawner>();
        foreach(NoteSpawner spawner in tmpSpawners)
        {
            spawners.Add(spawner.keyCode, spawner);
        }

        //송데이터의 노트 데이터를 큐에 시간순 등록

        List<NoteData> notes = SongSelector.instance.songData.notes;
        for(int i = 0;i < notes.Count; i++)
        {
            float tmpSpeed = 0;
            if(notes[i].speed>0)
                tmpSpeed = notes[i].speed;
            else
                tmpSpeed = NoteAssets.GetNote(notes[i].keyCode).speed;

            float timeScaled = notes[i].time / NoteAssets.GetNote(notes[i].keyCode).speed;
            notes[i] = new NoteData
            {
                keyCode = notes[i].keyCode,
                time = timeScaled,
            };
        }

        notes.Sort((x,y)=>x.time.CompareTo(y.time));

        foreach(NoteData note in notes) 
            noteQueue.Enqueue(note);        
    }
}
