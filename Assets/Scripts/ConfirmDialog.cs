using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Confirmation dialog class 
/// </summary>
public class ConfirmDialog : MonoBehaviour {
    /// <summary>
    /// dialog object
    /// </summary>
    public GameObject dialog;
    /// <summary>
    /// dialog title text object
    /// </summary>
    public Text title;
    /// <summary>
    /// dialog message text object
    /// </summary>
    public Text messageBox;
    /// <summary>
    /// When user select cancel or confirm, this method will called.
    /// </summary>
    /// <param name="cb">If canceled by user, this will be false, or confirmed, this will be true</param>
    public delegate void Callback(bool cb);
    /// <summary>
    /// UI conponents disabled when this dialog created.
    /// </summary>
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

