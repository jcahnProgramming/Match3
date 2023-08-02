using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsSaveLoad : Singleton<PlayerPrefsSaveLoad>
{
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void SaveInt(string key, int content)
    {
        PlayerPrefs.SetInt(key, content);
    }

    public int GetInt(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    public void SaveString(string key, string content)
    {
        PlayerPrefs.SetString(key, content);
    }
    public string GetString(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    public void SaveFloat(string key, float content)
    {
        PlayerPrefs.SetFloat(key, content);
    }

    public float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

}
