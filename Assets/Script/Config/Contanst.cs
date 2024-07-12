using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState 
{
    Prepare,
    Tutorial,
    ReadyToPlay,
    Playing,
    Pausing,
    End,
    Win
}

public class SceneName
{
    public const  string Initial = "Initial";
    public const string GamePlay = "GamePlay";
    public const string Main = "Main";
}

public class TagName
{
    public const string Player = "Player";
    public const string Bot = "Bot";
    public const string Collectable = "Collectable";
    public const string FishHead = "FishHead";
    public const string FishBody = "FishBody";
}

public class LayerName {
    public const string UI = "UI";
    public const string Default = "Default";
    public const string MapUI = "MapUI";
    public const string MapUITutorial = "MapUITutorial";
}

public class PlayerPrefKeys
{
    public const string isFirstOpen = "isFirstOpen";
    public const string isFirstTouch = "isFirstTouch";
    public const string isFirstShowKill = "isFirstShowKill";
    
    public const string isFirstKill = "isFirstKill";
    public const string isFirstDie = "isFirstDie";

    public const string IsFinishTutorial = "IsFinishTutorial";

    public const string GamePlayCount = "GamePlayCount";

    public const string UnlockStatusPref = "_UnlockStatus";

    public const string LastFishSelectID = "LastFishSelectID";

    public const string LastBladeSelectID = "LastBladeSelectID";

    public const string MapOceanTutorialFinished = "MapOceanTutorialFinished";

    public const string Day0FirstWin = "Day0FirstWin";
    public const string Day0FirstLose = "Day0FirstLose";
}

public class TrackingName
{
    public const string D0_FIRST_OPEN = "d0_first_open";
    public const string D0_FIRST_TIME_TOUCH_SCREEN = "d0_first_time_touch_screen";
    public const string D0_SHOW_KILL_TOTURIAL = "d0_show_kill_tutorial";
    public const string D0_FIRST_KILL = "d0_first_kill";
    public const string D0_FIRST_KILL_BEFORE_FIRST_DIE = "d0_first_kill_before_first_die";
    public const string D0_FIRST_DIE_BEFORE_FIRST_KILL = "d0_first_die_before_first_kill";
    public const string Rating = "rating";

    public const string First_game_win_on_D0 = "First_game_win_on_D0";
    public const string First_game_lose_on_D0 = "First_game_lose_on_D0";

    public const string Control_joystick_tutorial = "Control_joystick_tutorial";
    public const string Control_rush_tutorial = "Control_rush_tutorial";

    // Progression
    public const string progress_level_up = "progress_level_up";
    public const string progress_videoads_click = "progress_videoads_click";
    public const string new_level_ = "new_level_";
    public const string revive = "player_revive";

    // Map Progression
    public const string unlock_map1_d = "unlock_map1_d";
    public const string Unlock_map1 = "Unlock_map1";
    public const string unlock_map1_more_than_d3 = "unlock_map1_more_than_d3";
    public const string unlock_map2_d = "unlock_map2_d";
    public const string Unlock_map2 = "Unlock_map2";
    public const string unlock_map2_more_than_d3 = "unlock_map2_more_than_d3";
    public const string unlock_map3_d = "unlock_map3_d";
    public const string Unlock_map3 = "Unlock_map3";
    public const string unlock_map3_more_than_d6 = "unlock_map3_more_than_d6";

    // Ads
    public const string videoads_show_ready = "videoads_show_ready";
    public const string videoads_show_not_ready = "videoads_show_not_ready";
    public const string videoads_show_failed = "videoads_show_failed";
    public const string aj_rewarded_displayed = "aj_rewarded_displayed";
    public const string videoads_finish = "videoads_finish";
    public const string videoads_rewarded = "videoads_rewarded";
    public const string videoads_click = "videoads_click";
    public const string fullads_show_ready = "fullads_show_ready";
    public const string fullads_show_notready = "fullads_show_notready";
    public const string fullads_show_failed = "fullads_show_failed";
    public const string aj_inters_displayed = "aj_inters_displayed";
    public const string fullads_finish = "fullads_finish";
    public const string fullads_click = "fullads_click";
    public const string ads_skip_by_noads = "ads_skip_by_noads";

    // IAP
    public const string iap_purchase_success = "iap_purchase_success";
    public const string iap_purchase_failed = "iap_purchase_failed";
}

public class TrackingParam
{
    public const string reward_source = "reward_source";
    public const string level_id = "level_id";
    public const string game_count_last_level_up = "game_count_last_level_up";
}

public class TrackingValue
{
    public const string Double_Prize = "Double_Prize";
    public const string Revive = "Revive";
    public const string Unlock_Fish = "Unlock_Hero";
    public const string Unlock_Blade = "Unlock_Glove";
    public const string Unlock_Ocean = "Unlock_Map";
    public const string day = "day";
}

public class AppsflyerEvent
{
    public const string af_inters_logicgame = "af_inters_logicgame";
    public const string af_inters_api_show = "af_inters_api_show";
    public const string af_inters_successfullyloaded = "af_inters_successfullyloaded";
    public const string af_inters_displayed = "af_inters_displayed";
    
    public const string af_rewarded_logicgame = "af_rewarded_logicgame";
    public const string af_rewarded_successfullyloaded = "af_rewarded_successfullyloaded";
    public const string af_rewarded_displayed = "af_rewarded_displayed";
}

public static class Development
{
    public static bool isEnableDebug = true;

    public static void Log(string debugLog,LogType type = LogType.Log)
    {
        if (isEnableDebug)
        {
            switch(type)
            {
                case LogType.Log:
                    Debug.Log(debugLog);
                    break;
                case LogType.Warning:
                    Debug.LogWarning(debugLog);
                    break;
                case LogType.Error:
                default:
                    Debug.LogError(debugLog);
                    break;
            }
        }
    }
}

public static class ParticleSystemUtil
{
    public static ParticleSystem SetActive(this ParticleSystem mParticle, bool isActive)
    {
        if (!mParticle.main.loop)
        {
            if (isActive)
            {
                mParticle.Simulate(0.0f, true, true);
                mParticle.Play();
            }
        }
        return mParticle;
    }
}
