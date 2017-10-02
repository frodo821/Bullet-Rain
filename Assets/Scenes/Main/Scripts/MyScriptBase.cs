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
        worldLimitMax = main.ScreenToWorldPoint(new Vector3(main.pixelWidth, main.pixelHeight));
    }
}
