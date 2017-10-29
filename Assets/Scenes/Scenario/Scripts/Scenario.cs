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
    public static bool canQueueImage = false;
    public Button skipall;
    public Text text;
    public Text next;
    public RawImage back;
    public Image blinder;
    bool sceneLoading = false;
    Queue<Texture2D> bgImages = new Queue<Texture2D>();
    Queue<string> scenario;
    string lt = string.Empty;
    Coroutine scen;
    Coroutine blink;

	void Start () {
#if UNITY_EDITOR
        PlayerPrefs.DeleteKey("user_has_ever_played");
#endif
        scenario = new Queue<string>(
            Resources.Load<TextAsset>(scenarioName + "/scenario").text.Split(
                new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries));
        if (!canQueueImage)
        {
            back.texture = Resources.Load<Texture2D>(scenarioName + "/main");
        }else
        {
            var idx = 0;
            while (true)
            {
                var tmp = Resources.Load<Texture2D>(scenarioName + "/" + idx);
                if(tmp == null)
                {
                    break;
                }
                idx++;
                bgImages.Enqueue(tmp);
            }
        }
        scen = StartCoroutine("Read");
        if (scenarioName == "prologue")
        {
            if (PlayerPrefs.GetInt("user_has_ever_played", 0) == 0)
            {
                skipall.gameObject.SetActive(false);
                PlayerPrefs.SetInt("user_has_ever_played", 1);
            }
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
            lt = scenario.Dequeue().Replace("[seh]", "--");
            try
            {
                if (canQueueImage)
                {
                    back.texture = bgImages.Dequeue();
                }
            }
            catch (InvalidOperationException) { }
        }
        catch (InvalidOperationException)
        {
            LoadScene();
        }
        var lcs = new Queue<char>(lt.ToCharArray());
        while (true)
        {
            char c;
            string nxts = "";
            try
            {
                c = lcs.Dequeue();
            }
            catch (InvalidOperationException) { break; }
            if (c == ':')
            {
                nxts += lcs.Dequeue();
                if (nxts == ":")
                    yield return new WaitForSeconds(1f);
                else
                    text.text += nxts;
                continue;
            } else if (c == '[')
            {
                var chrs = new string(lcs.ToArray());
                if (chrs.Split(new char[] { ']'})[0] == "sec")
                {
                    while (true)
                    {
                        var dec = lcs.Dequeue();
                        if (dec == ']') break;
                    }
                    text.text += ":";
                    yield return new WaitForSeconds(0.05f);
                    text.text += ":";
                    yield return new WaitForSeconds(0.05f);
                }else
                {
                    text.text += "[";
                    yield return new WaitForSeconds(0.05f);
                }
                continue;
            }
            text.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        blink = StartCoroutine("Blink");
    }

    public void Skip()
    {
        var tlt = lt.Replace("::", "").Replace("[sec]", "::");
        if (text.text == tlt)
        {
            StopCoroutine(blink);
            next.gameObject.SetActive(false);
            scen = StartCoroutine("Read");
        }
        else
        {
            StopCoroutine(scen);
            text.text = tlt;
            blink = StartCoroutine("Blink");
        }
    }

    public void SkipAll()
    {
        LoadScene();
    }

    private void LoadScene()
    {
        if (sceneLoading) return;
        sceneLoading = true;
        StopAllCoroutines();
        StartCoroutine("Fadeout");
    }

    private IEnumerator Fadeout()
    {
        blinder.color = new Color(0, 0, 0, 0);
        blinder.gameObject.SetActive(true);
        while (true)
        {
            var c = blinder.color;
            c.a += 0.01f;
            blinder.color = c;
            yield return new WaitForSeconds(0.02f);
            if (c.a >= 1) break;
        }
        var async = SceneManager.LoadSceneAsync(nextSceneName);
    }
}
