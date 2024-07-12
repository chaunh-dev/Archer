using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsHelper
{
    public static void SetInt(string key, int value)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WebCommunication.instance.SetPlayerPrefInt(key, value);
        }
        else
        {
            PlayerPrefs.SetInt(key, value);
        }
    }

    public static void SetString(string key, string value)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WebCommunication.instance.SetPlayerPrefString(key, value);
        }
        else
        {
            PlayerPrefs.SetString(key, value);
        }
    }

    public static void SetFloat(string key, float value)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WebCommunication.instance.SetPlayerPrefFloat(key, value);
        }
        else
        {
            PlayerPrefs.SetFloat(key, value);
        }
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return WebCommunication.instance.GetPlayerPrefInt(key, defaultValue);
        }
        else
        {
            return PlayerPrefs.GetInt(key, defaultValue);
        }
    }

    public static string GetString(string key, string defaultValue = "")
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return WebCommunication.instance.GetPlayerPrefString(key, defaultValue);
        }
        else
        {
            return PlayerPrefs.GetString(key, defaultValue);
        }
    }

    public static float GetFloat(string key, float defaultValue = 0.0f)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return WebCommunication.instance.GetPlayerPrefFloat(key, defaultValue);
        }
        else
        {
            return PlayerPrefs.GetFloat(key, defaultValue);
        }
    }

    public static bool HasKey(string key)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            return WebCommunication.instance.HasPlayerPrefKey(key);
        }
        else
        {
            return PlayerPrefs.HasKey(key);
        }
    }

    public static void DeleteKey(string key)
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WebCommunication.instance.DeletePlayerPrefKey(key);
        }
        else
        {
            PlayerPrefs.DeleteKey(key);
        }
    }

    public static void DeleteAll()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WebCommunication.instance.ClearAllData();
        }
        else
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public static void Save()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            WebCommunication.instance.SavePlayerPref();
        }
        else
        {
            PlayerPrefs.Save();
        }
    }
}
