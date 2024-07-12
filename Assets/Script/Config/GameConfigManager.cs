using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GFramework.Utils;
using System.Linq;

public class GameConfigManager : SingletonMono<GameConfigManager>
{
    [Space(10)]
    [Header("Helpers")]
    public ArrowStatsConfigHelper arrowStats;
    public RagdollControlConfigHelper ragdoll;
    public GameplayConfigHelper gameplay;
    public BotConfigHelper bot;
    public LevelConfigHelper level;
    public AIProfileConfigHelper aIProfile;
    public CharacterStatsConfigHelper stats;

#if UNITY_EDITOR

    [ContextMenu("ClearPlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefsHelper.DeleteAll();
    }
#endif
}

public abstract class ConfigBase<TDataRecord> : ScriptableObject, IEnumerable<TDataRecord> where TDataRecord : class
{
    [SerializeField]
    public List<TDataRecord> records;

    public TDataRecord this[int key]
    {
        get
        {
            if (records == null || records.Count <= key)
                return null;
            return records[key];
        }
        set
        {
            if (records != null && records.Count > key)
                records[key] = value;
        }
    }

    public IEnumerator<TDataRecord> GetEnumerator()
    {
        return records.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public abstract List<TDataRecord> LoadDataFromText(string textAsset);
    public abstract void SetDataFromText(string text);
    public int Count
    {
        get
        {
            return records.Count;
        }
    }
}
