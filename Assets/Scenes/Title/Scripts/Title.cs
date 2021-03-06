﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title : MonoBehaviour {
    public Text txt;
    public Text rtitle;
    public Dropdown gamemode;
    public static GameMode mode = GameMode.Normal;
    public Text level;
    public Text exp;
    public Text cash;
    public ConfirmDialog dialog;
    public Image fadeout;
    private AsyncOperation async = null;

    void Start()
    {
        mode = (GameMode)PlayerPrefs.GetInt("user_difficulty", (int)GameMode.Normal);
        gamemode.options.Clear();
        foreach(var i in System.Enum.GetValues(typeof(GameMode)))
        {
            gamemode.options.Add(
                new Dropdown.OptionData(
                    "Difficulty: " + i.ToString()
                    ));
        }
        Time.timeScale = 0f;
        gamemode.value = (int)mode - 1;
        ShowRank();
        PlayerStats.Initialize();
        ShowUserStats();
    }

    void ShowUserStats()
    {
        level.text = "Level " + PlayerStats.level;
        var req = PlayerStats.requireExp;
        var cur = PlayerStats.freeExp;
        exp.text = cur + "/" + req + " exp";
        cash.text = "game cash: " + PlayerStats.UserCash + " coins";
    }

    void ShowRank()
    {
        var top = PlayerPrefs.GetInt("ranktop", -1);
        var second = PlayerPrefs.GetInt("ranksecond", -1);
        var third = PlayerPrefs.GetInt("rankthird", -1);
        var rankText = top == -1 ? string.Empty : "1. " + top + "pts\n";
        rankText += second == -1 ? string.Empty : "2. " + second + "pts\n";
        rankText += third == -1 ? string.Empty : "3. " + third + "pts";
        if (rankText == string.Empty)
        {
            rtitle.text = string.Empty;
        }
        txt.text = rankText;
    }

    public void ResetRank()
    {
        PlayerPrefs.DeleteKey("ranktop");
        PlayerPrefs.DeleteKey("ranksecond");
        PlayerPrefs.DeleteKey("rankthird");
        ShowRank();
    }

    public void OnGameStartSelected()
    {
        StartCoroutine("GameStart");
    }

    private IEnumerator GameStart()
    {
        fadeout.color = new Color(0, 0, 0, 0);
        fadeout.gameObject.SetActive(true);
        while (true)
        {
            var c = fadeout.color;
            c.a += 0.01f;
            fadeout.color = c;
            if (fadeout.color.a >= 1) break;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        async = SceneManager.LoadSceneAsync("template");
    }

    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }

    public void OnGameModeSelected()
    {
        mode = (GameMode)gamemode.value + 1;
        PlayerPrefs.SetInt("difficulty", (int)mode);
    }

    public void Shop()
    {

    }

    public void ClearGameData()
    {
        dialog.ShowDialog("Reset your game",clear , "Are you sure to reset your game? (You can't undo this operation.)");
    }

    void clear(bool res)
    {
        if (!res) return;
        PlayerPrefs.DeleteAll();
        ShowRank();
        PlayerStats.Initialize();
        ShowUserStats();
    } 
}

public enum GameMode
{
    Easy = 1,
    Normal = 2,
    Hard = 3,
    Lunatic = 4,
    Ultimate = 5
}
