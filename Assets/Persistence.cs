using UnityEngine;

public static class Persistence
{
    public static bool IsOldUser(SaveKey anyKey)
    {
        return PlayerPrefs.HasKey(anyKey.ToString());
    }

    public static void Save(SaveKey key, int value)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }

    public static void Save(this int value, SaveKey key)
    {
        PlayerPrefs.SetInt(key.ToString(), value);
    }

    public static void Save(SaveKey key, string value)
    {
        PlayerPrefs.SetString(key.ToString(), value);
    }

    public static void SetDefaultValues()
    {
        PlayerPrefs.SetInt(SaveKey.Currency.ToString(), 50);
        PlayerPrefs.SetString(SaveKey.Scene.ToString(), "level1");
    }
}
