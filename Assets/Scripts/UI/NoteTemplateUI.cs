using System;
using UnityEngine;
using UnityEngine.UI;

public class NoteTemplateUI : MonoBehaviour
{
    [SerializeField] private Text noteText;
    [SerializeField] private Text noteTitle;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void SetNote(NoteData note)
    {
        noteTitle.text = note.noteTitle;
        noteText.text = note.noteText;
    }
}
