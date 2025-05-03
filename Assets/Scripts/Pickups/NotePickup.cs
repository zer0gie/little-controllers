using System;
using System.Globalization;
using UnityEngine;

public class NotePickup : MonoBehaviour
{
    [SerializeField] private string noteText;
    [SerializeField] private int noteID;
    [SerializeField] private string noteTitle;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.TryGetComponent(out MovementController playerController))
        {
            var note = new NoteData
            {
                noteText = noteText,
                noteID = noteID,
                noteTitle = noteTitle,
                getTime = DateTime.Now.ToString(CultureInfo.CurrentCulture)
            };
            NoteLogger.Instance.TryAddNote(note);
            Destroy(gameObject);
        }
    }
}
