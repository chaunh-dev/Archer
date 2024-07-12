using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelConfigHelper : MonoBehaviour
{
    private int tempIndex = 0;
    private LevelConfigData GetConfigByIndex(int _index = -1)
    {
        if (_index == -1)
        {
            _index = tempIndex;
        }
        else tempIndex = _index;
        return ConfigManager.instance.GetConfig<LevelConfigCollection>()[_index];
    }

    public int GetLevel()
    {
        return GetConfigByIndex().level;
    }

    public int GetNumberOfSingleBot()
    {
        return GetConfigByIndex().singleBot;
    }

    public int GetNumberOfDoubleBot()
    {
        return GetConfigByIndex().doubleBot;
    }

    public int GetNumberOfTrippleBot()
    {
        return GetConfigByIndex().trippleBot;
    }

    public int GetNumberOfVerticalMovingBot()
    {
        return GetConfigByIndex().verticalMovingBot;
    }

    public int GetNumberOfHorizontalMovingBot()
    {
        return GetConfigByIndex().horizontalMovingBot;
    }

    public int GetNumberOfBlinkBot()
    {
        return GetConfigByIndex().blinkBot;
    }

    public int GetBotHealth()
    {
        return GetConfigByIndex().botHealth;
    }

    public int GetBossHealth()
    {
        return GetConfigByIndex().bossHealth;
    }

    public bool IsBossUsingSkin()
    {
        return GetConfigByIndex().bossSkin;
    }

    public string GetBotAIProfile()
    {
        return GetConfigByIndex().botAIProfile;
    }

    public string GetBossAIProfile()
    {
        return GetConfigByIndex().bossAIProfile;
    }

    public bool IsBotUsingHeadArmor()
    {
        return GetConfigByIndex().botHeadArmor;
    }

    public bool IsBotUsingChestArmor()
    {
        return GetConfigByIndex().botChestArmor;
    }
    public bool IsBotUsingLegArmor()
    {
        return GetConfigByIndex().botLegArmor;
    }

    public float GetTime()
    {
        return GetConfigByIndex().time;
    }

    public int GetHighestLevel()
    {
        return ConfigManager.instance.GetConfig<LevelConfigCollection>().Count;
    }

    public int GetTotalBot()
    {
        return GetConfigByIndex().singleBot + GetConfigByIndex().doubleBot + GetConfigByIndex().trippleBot +
                GetConfigByIndex().verticalMovingBot + GetConfigByIndex().horizontalMovingBot + GetConfigByIndex().blinkBot;
    }

    public LevelBase GetLevelData(int _levelindex)
    {
        LevelBase levelBase = new LevelBase();
        GetConfigByIndex(_levelindex);
        levelBase.level = GetLevel();
        levelBase.wave = GetTotalBot() + 1;
        levelBase.singleBot = GetNumberOfSingleBot();
        levelBase.doubleBot = GetNumberOfDoubleBot();
        levelBase.trippleBot = GetNumberOfTrippleBot();
        levelBase.verticalMovingBot = GetNumberOfVerticalMovingBot();
        levelBase.horizontalMovingBot = GetNumberOfHorizontalMovingBot();
        levelBase.blinkBot = GetNumberOfBlinkBot();
        levelBase.time = GetTime();

        return levelBase;
    }
}
