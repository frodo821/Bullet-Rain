using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Scenario : MonoBehaviour {
    public Text text;
    public static string scenarioName = "prologue"; 
    string scenario;
    string lt = string.Empty;
    int line = 0;
    Coroutine col;

	void Start () {
        scenario = Resources.Load<TextAsset>(scenarioName).text;
        //print(scenario);
        col = StartCoroutine("Read");
	}
	
    void Update()
    {
        
    }

    IEnumerator Read()
    {
        lt = scenario;//[line];
        var lcs = lt.ToCharArray();
        foreach(var c in lcs)
        {
            text.text += c;
            if(c == '\n') yield return new WaitForSeconds(0.2f);
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void Skip()
    {
        StopCoroutine(col);
        text.text = lt;
    }
}
