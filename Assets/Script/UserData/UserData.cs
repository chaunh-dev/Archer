using System;
using System.Collections;
using System.Collections.Generic;
using GFramework.GameData;
using UnityEngine;

public class UserData : UserDataBase
{
    public int Level;
    public int PlayerLevel;
    public int TotalKill;
    public int StatPoint;
    public int ArmorPoint;
    public int HealthPoint;
    public int HealthRegenPoint;
    public int ManaPoint;
    public int ManaRegenPoint;
    public float CriticalChance;
    public int LoseMatch;
    public int TutorialStep;
    public int WinMatch;
    public int MatchStarted;
    public int HighestRoundReached;
    public int ItemUnlockPriority;
    public int playerHelmetIndex;
    public int playerChestArmorIndex;
    public int playerLegsArmorIndex;

    public string PlayerName;
    public bool EnableMusic;
    public bool EnableSound;
    public bool EnableHaptic;
    public bool greenScreenMainMenu;
    public bool greenScreenInGame;
    public bool greenScreenEndGame;
    public bool greenScreenExtraGift;
    public int aimLength;

    public List<string> listShopItemUnlocked;
    public List<string> listArrowUnlocked;
    public List<string> listArrowPassProgression;
    public List<string> listMapUnlocked;
    public List<string> listMapPassProgression;


    public ProgressionUserInfo DefaultProgressionInfo;

    public int currentProgressIndex;
    public int currentUnlockingFragment;
    public string currentArowUsing;
    public string selectedFlagID = "";
    public string selectedTreatID = "TR01";
    public List<string> selectedCharacterID = new List<string>()
    { "CR01", "CR14", "CR15", "CR16", "CR17", "CR18", "CR19", "CR20", "CR21", "CR22", "CR23", "CR24" };

    public bool IsConvertedOldData = false;

    public int GameCountBetweenLevelProgress;
    public int restartRound;

    public bool noAdsEnabled;

    public UserData()
    {
        SetDefault();
    }

    public void SetDefault()
    {
        Level = 1;
        PlayerLevel = 1;
        TotalKill = 0;
        StatPoint = 0;
        ArmorPoint = 0;
        HealthPoint = GameConfigManager.instance.stats.GetDefaultHP();
        HealthRegenPoint = GameConfigManager.instance.stats.GetDefaultHPRegen();
        ManaPoint = GameConfigManager.instance.stats.GetDefaultMP();
        ManaRegenPoint = GameConfigManager.instance.stats.GetDefautlMPRegen();
        CriticalChance = (float)GameConfigManager.instance.stats.GetDefaultCriticalChance() / 100;
        LoseMatch = 0;
        WinMatch = 0;
        MatchStarted = 0;
        TutorialStep = 0;
        GameCountBetweenLevelProgress = 0;
        restartRound = 1;
        HighestRoundReached = 0;
        ItemUnlockPriority = 0;
        playerHelmetIndex = -1;
        playerChestArmorIndex = -1;
        playerLegsArmorIndex = -1;

        currentProgressIndex = 0;

        listArrowUnlocked = new List<string>();
        listArrowPassProgression = new List<string>();

        listMapPassProgression = new List<string>();
        listMapUnlocked = new List<string>();

        listShopItemUnlocked = new List<string>();

        DefaultProgressionInfo = new ProgressionUserInfo();

        PlayerName = "Player";
        EnableMusic = true;
        EnableSound = true;
        EnableHaptic = true;

        greenScreenMainMenu = false;
        greenScreenInGame = false;
        greenScreenEndGame = false;
        greenScreenExtraGift = false;
        aimLength = 15;


        currentArowUsing = "default";
    }

    public void NextProgress()
    {
        currentProgressIndex++;
        currentUnlockingFragment = 0;
    }

    public void UnlockProgressToIndex(int index)
    {
        //Debug.Log("unlock progress to index " + index + " " + currentProgressIndex);
        //if (index < currentProgressIndex)
        //{
        //    return;
        //}

        //while (index > currentProgressIndex)
        //{
        //    string currentProgressionID = GameConfigManager.instance.unlockProgressHelper.GetUnlockingItemID(
        //    UserDataService.instance.UserData.currentProgressIndex);
        //    UnlockShopItem(currentProgressionID);

        //    currentProgressIndex++;
        //    currentUnlockingFragment = 0;
        //}
    }

    public void UnlockShopItem(string ID)
    {
        listShopItemUnlocked.Add(ID);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void FromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

}


[Serializable]
public class ProgressionUserInfo
{
    public string CurrentIDProgression;
    public string CurrentTypeProgression;
    public int CurrentIndexProgression;
    public int CurrentFragmentProgression;
    public int TargetFragmentProgression;
    public int LastFragmentProgression;
    public bool IsFillterProgression;
}
