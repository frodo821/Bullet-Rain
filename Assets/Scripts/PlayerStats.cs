using UnityEngine;

public class PlayerStats {
    public static int UserExp { get; private set; }
    public static int level { get; private set; }
    public const int baseExp = 100;
    public static int UserCash { get; private set; }
    static string earnedUpgrade;
    
    public static void Initialize()
    {
        UserExp = PlayerPrefs.GetInt("user_exp", 0);
        level = PlayerPrefs.GetInt("user_level", 0);
        UserCash = PlayerPrefs.GetInt("user_cash", 0);
        earnedUpgrade = PlayerPrefs.GetString("upgrades", string.Empty);
        Debug.Log("Cur Exp" + UserExp);
        Debug.Log("Next Exp" + GetCurrentFreeExp(level));
#if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
#endif
    }

    public static string[] GetUpgradesAsArray()
    {
        return earnedUpgrade.Split(',');
    }

    public static void SetUpgradesToString(string[] upgs)
    {
        earnedUpgrade = string.Join(",", upgs);
    }

    public static int GetRequiredTotallyExp(int lvl)
    {
        if (lvl == 0) return baseExp;
        if (lvl < 0) return 0;
        return GetRequiredExp(lvl) + GetRequiredTotallyExp(lvl - 1);
    }

    public static int GetRequiredExp(int lvl)
    {
        return (int)(Mathf.Pow(1.1f, lvl) * baseExp);
    }

    public static int GetCurrentFreeExp(int lvl)
    {
        if (level > 0)
        {
            return UserExp - GetRequiredTotallyExp(lvl - 1);
        }
        return UserExp;
    }

    public static void ApplayEarnedExp(int experience)
    {
        UserExp += experience;
        levelup();
        PlayerPrefs.SetInt("user_exp", UserExp);
    }

    static void levelup()
    {
        if (GetCurrentFreeExp(level + 1) > 0)
        {
            level++;
            PlayerPrefs.SetInt("user_level", level);
            levelup();
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
