using System;
using UnityEngine;

public class DiaryUI : MonoBehaviour
{
    [SerializeField] private Transform notesLayout;
    [SerializeField] private Transform noteItemTemplate;

    private bool _isDiaryOpened;

    private void Start()
    {
        if (NoteLogger.Instance.GetPlayerNotes().notes.Count > 0)
        {
            var playerNotes = NoteLogger.Instance.GetPlayerNotes();

            foreach (var note in playerNotes.notes)
            {
                AddNoteToDiary(note);
            }
        }
        InputManager.Instance.OnDiaryOpenAction += InputManager_OnDiaryOpenAction;
        NoteLogger.Instance.OnNoteAdded += NoteLogger_OnNoteAdded;
        _isDiaryOpened = false;
        gameObject.SetActive(false);
    }

    private void NoteLogger_OnNoteAdded(object sender, NoteLogger.OnNoteAddedEventArgs e)
    {
        AddNoteToDiary(e.Note);
    }

    private void InputManager_OnDiaryOpenAction(object sender, EventArgs e)
    {
        _isDiaryOpened = !_isDiaryOpened;
        if (_isDiaryOpened)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void AddNoteToDiary(NoteData note)
    {
        var noteTransform = Instantiate(noteItemTemplate, notesLayout);
        noteTransform.gameObject.SetActive(true);
        var noteUI = noteTransform.GetComponent<NoteTemplateUI>();
        if (noteUI != null)
        {
            noteUI.SetNote(note);
        }
        if (_isDiaryOpened == true) Show();
    }
    private void Show()
    {
        gameObject.SetActive(true);
        notesLayout.gameObject.SetActive(true);
        foreach (Transform child in notesLayout)
        {
            if (child == noteItemTemplate) continue;
            child.gameObject.SetActive(true);
        }
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnDiaryOpenAction -= InputManager_OnDiaryOpenAction;
        NoteLogger.Instance.OnNoteAdded -= NoteLogger_OnNoteAdded;
    }
}
