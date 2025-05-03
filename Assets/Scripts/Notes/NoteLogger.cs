using System.IO;
using UnityEngine;

public class NoteLogger : MonoBehaviour
{
    public static NoteLogger Instance { get; private set; }
    
    private const string LOG_FOLDER = "Notes";
    
    private const string LOG_NAME = "notelog.json";

    private PlayerNotes _playerNotes;

    private void Awake()
    { 
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        } 
        Instance = this;
        _playerNotes = LoadPlayerNotes();
    }

    public void TryAddNote(NoteData note)
    {
        if (IsNoteAlreadyExist(note))
        {
            Debug.Log("Note " + note.noteID + " already in player notes");
            return;
        }
        
        _playerNotes.notes.Add(note);
        SavePlayerNotes();
        Debug.Log(note.noteID + "NOTE ADDED");
    }

    private bool IsNoteAlreadyExist(NoteData note)
    {
        return (_playerNotes.notes.Exists(n => n.noteID == note.noteID));
    }

    private PlayerNotes LoadPlayerNotes()
    {
        var path = Path.Combine(Application.persistentDataPath, LOG_FOLDER, LOG_NAME);

        if (!File.Exists(path))
        {
            Debug.Log("LOAD: new PlayerNotes created");
            return new PlayerNotes();
        }

        var json = File.ReadAllText(path);
        Debug.Log("LOAD: FromJson " + path);
        return JsonUtility.FromJson<PlayerNotes>(json);
    }

    private void SavePlayerNotes()
    {
        var directoryPath = Path.Combine(Application.persistentDataPath, LOG_FOLDER);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        var json = JsonUtility.ToJson(_playerNotes, true);

        var path = Path.Combine(Application.persistentDataPath, LOG_FOLDER, LOG_NAME);

        File.WriteAllText(path, json);
        Debug.Log("SAVE: saved to " + path);
    }
    private void OnLog()
    {
            
    }
}
