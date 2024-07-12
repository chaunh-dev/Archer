using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : SingletonMono<ConfigManager>
{
    public List<ConfigData> listCofData;

    public TCollection GetConfig<TCollection>() where TCollection : ScriptableObject
    {
        if (listCofData == null)
            return null;

        for (int i = 0; i < listCofData.Count; i++)
        {
            if (listCofData[i].config is TCollection)
                return (TCollection)listCofData[i].config;
        }

        return null;
    }
}

[Serializable]
public class ConfigData
{
    public ScriptableObject config;
}

