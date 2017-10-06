using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIButton : MonoBehaviour {

    void OnShowUI(GameObject sender)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        var score = sender.GetComponent<Player>().score;
        var top = PlayerPrefs.GetInt("ranktop", -1);
        var second = PlayerPrefs.GetInt("ranksecond", -1);
        var third = PlayerPrefs.GetInt("rankthird", -1);
        if(third < score)
        {
            if(second < score)
            {
                if(top < score)
                {
                    sender.GetComponent<Player>().highscoreMarked = true;
                    PlayerPrefs.SetInt("ranktop", score);
                    if(top != -1)
                        PlayerPrefs.SetInt("ranksecond", top);
                    if(second != -1)
                        PlayerPrefs.SetInt("rankthird", second);
                }
                else
                {
                    PlayerPrefs.SetInt("ranksecond", score);
                    if(second != -1)
                        PlayerPrefs.SetInt("rankthird", second);
                }
            }
            else
            {
                PlayerPrefs.SetInt("rankthird", score);
            }
        }
    }
    public void ReturnTitle()
    {
        SceneManager.LoadScene("title");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
