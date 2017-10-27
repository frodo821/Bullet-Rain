using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public GameObject dialog;
    public bool inPause = false;
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inPause = !inPause;
            Time.timeScale = 1f;
            dialog.gameObject.SetActive(false);
        }
        if (inPause)
        {
            Time.timeScale = 0f;
            dialog.gameObject.SetActive(true);
        }
	}

    public void CloseMenu()
    {
        Time.timeScale = 1f;
        inPause = false;
        dialog.gameObject.SetActive(false);
    }
}
