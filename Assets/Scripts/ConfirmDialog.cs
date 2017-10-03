using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialog : MonoBehaviour {
    public GameObject dialog;
    public Text title;
    public Text messageBox;
    public delegate void Callback(bool cb);
    public Selectable[] disableOnDialogCreated;
    Callback callback;

    // Use this for initialization
    void Start () {
        dialog.SetActive(false);
	}

    public void ShowDialog(string msgTitle, Callback c, string message = "")
    {
        foreach(Selectable b in disableOnDialogCreated)
        {
            b.interactable = false;
        }
        title.text = msgTitle;
        messageBox.text = message;
        dialog.SetActive(true);
        callback = c;
    }
	
    public void OnCanceled()
    {
        dialog.SetActive(false);
        callback(false);
        CleanUp();
    }

    public void OnConfirmed()
    {
        dialog.SetActive(false);
        callback(true);
        CleanUp();
    }

    void CleanUp()
    {
        foreach (Selectable b in disableOnDialogCreated)
        {
            b.interactable = true;
        }
        callback = null;
    }
}

