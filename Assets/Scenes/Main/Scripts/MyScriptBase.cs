using UnityEngine;

public class MyScriptBase : MonoBehaviour
{
    protected Vector3 worldLimitMin;
    protected Vector3 worldLimitMax;
    protected Camera main;

    protected void GetWorldLimit()
    {
        main = Camera.main;
        worldLimitMin = main.ScreenToWorldPoint(new Vector3(0, 0));
        worldLimitMax = main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    protected virtual void OnFinishedGame(GameObject sender) { }
}
