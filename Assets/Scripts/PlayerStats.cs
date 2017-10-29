using UnityEngine;

public class PlayerStats {
    public static int requireExp { get; private set; }
    public static int freeExp { get; private set; }
    public static int level { get; private set; }
    public const int baseExp = 100;
    public static int UserCash { get; private set; }
    static string earnedUpgrade;
    
    public static void Initialize()
    {
        requireExp = PlayerPrefs.GetInt("user_req_exp", baseExp);
        freeExp = PlayerPrefs.GetInt("user_free_exp", 0);
        level = PlayerPrefs.GetInt("user_level", 0);
        UserCash = PlayerPrefs.GetInt("user_cash", 0);
        earnedUpgrade = PlayerPrefs.GetString("upgrades", string.Empty);
        Debug.Log("Cur Exp" + freeExp);
        Debug.Log("Next Exp" + (requireExp - freeExp));
    }

    public static string[] GetUpgradesAsArray()
    {
        return earnedUpgrade.Split(',');
    }

    public static void SetUpgradesToString(string[] upgs)
    {
        earnedUpgrade = string.Join(",", upgs);
    }

    public static int GetRequiredExp()
    {
        return (int)(Mathf.Pow(1.1f, level) * baseExp);
    }

    public static void ApplayEarnedExp(int experience)
    {
        freeExp += experience;
        levelup();
        PlayerPrefs.SetInt("user_free_exp", freeExp);
    }

    static void levelup()
    {
        if (requireExp < freeExp)
        {
            level++;
            freeExp -= requireExp;
            requireExp = GetRequiredExp();
            levelup();
        }
        else
        {
            PlayerPrefs.SetInt("user_level", level);
            PlayerPrefs.SetInt("user_req_exp", requireExp);
        }
    }

    public static bool PayCash(int c)
    {
        if (c > UserCash) return false;
        UserCash -= c;
        PlayerPrefs.SetInt("user_cash", UserCash);
        return true;
    }

    public static void AddCash(int c)
    {
        UserCash += c;
        PlayerPrefs.SetInt("user_cash", UserCash);
    }
}
