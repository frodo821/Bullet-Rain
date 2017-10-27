using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class Scenario : MonoBehaviour
{
    public static string scenarioName = "prologue";
    public static string nextSceneName = "title";
    public Button skipall;
    public Text text;
    public Text next;
    Queue<string> scenario;
    string lt = string.Empty;
    Coroutine scen;
    Coroutine blink;

	void Start () {
        scenario = new Queue<string>(
            Resources.Load<TextAsset>(scenarioName).text.Split(
                new string[] { "--", "::" }, StringSplitOptions.RemoveEmptyEntries));
        scen = StartCoroutine("Read");
        if(PlayerPrefs.GetInt("user_has_ever_played", 0) == 0)
        {
            skipall.gameObject.SetActive(false);
            PlayerPrefs.SetInt("user_has_ever_played", 1);
        }
	}

    IEnumerator Blink()
    {
        if (scenario.Count == 0)
        {
            next.text = "Click to Start!";
        }else
        {
            next.text = "Click to Next";
        }
        next.gameObject.SetActive(true);
        for (var _ = 0; _ < 200; _++)
        {
            var c = next.color;
            c.a = Mathf.Sin(Time.time * 5) * 0.5f + 0.5f;
            next.color = c;
            yield return null;
        }
        next.gameObject.SetActive(false);
        scen = StartCoroutine("Read");
        StopCoroutine(blink);
    }

    IEnumerator Read()
    {
        text.text = string.Empty;
        lt = string.Empty;
        try
        {
            lt = scenario.Dequeue().Replace("[seh]", "--").Replace("[sec]", "::");
        }
        catch (InvalidOperationException)
        {
            SceneManager.LoadScene(nextSceneName);
            StopAllCoroutines();
        }
        var lcs = lt.ToCharArray();
        foreach (var c in lcs)
        {
            text.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        blink = StartCoroutine("Blink");
    }

    public void Skip()
    {
        if (text.text == lt)
        {
            StopCoroutine(blink);
            next.gameObject.SetActive(false);
            scen = StartCoroutine("Read");
        }
        else
        {
            StopCoroutine(scen);
            text.text = lt;
            blink = StartCoroutine("Blink");
        }
    }
    public void SkipAll()
    {
        StopAllCoroutines();
        SceneManager.LoadScene(nextSceneName);
    }
}
