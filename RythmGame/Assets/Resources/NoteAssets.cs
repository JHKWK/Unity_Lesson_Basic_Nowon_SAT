using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteAssets : MonoBehaviour
{
    static NoteAssets _instance;
        public static NoteAssets instance
    {
        get
        {
            if (_instance == null)
                _instance = Instantiate(Resources.Load<NoteAssets>("NoteAssets"));
            return _instance;
        }
    }

    public List<Note> notes     = new List<Note>();

    public static Note GetNote(KeyCode keyCode) =>
        instance.notes.Find(x => x.keyCode == keyCode);
}
