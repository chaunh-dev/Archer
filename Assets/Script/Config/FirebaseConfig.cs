using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FirebaseConfig
{
#region ABTest

    public bool AB_Revive;
    public bool AB_Offer;
    public bool AB_ZoomCamera;
    public bool AB_Matching;
#endregion

    public int AdsInterval;
    public int AdsFirstShow;

    public string AppId;
    public bool EnableRating;

    public OfferInfo offerInfo;
    public SessionTutorial sessionTutorial;

    public int min_value_revenue;
    public int config_max_day_send_revenue;

    public float GamePlayMinScaleSpeed;
    public float GamePlayMaxScaleSpeed;
    public float GamePlayStepScaleSpeed;
    public int GamePlayKillIncreaseSpeed;

    public float character_move_speed;
    public float power_strike_hold_time;
    public float strike_cooldown;
    public int character_damage;
    public int character_powerstrike_damage;
    public int character_max_health;
    public int character_dash_speed;
    public float input_disable_dash_strike;
    public float scale_per_kill;
    public LevelUpDatas level_up_datas;
    public CollectableData collectable_data;
    public float character_max_scale;
    public float character_hp_cap;
    public KnockBackData knock_back_data;
    public HeroRemoteDatas hero_datas;
    public MapConfigs mapConfigs;
    public BannerAdsSettings banner_ads_settings;
    public RespawnDatas respawn_datas;
    public EnergyData energy_data;
    public SlashData slash_data;

    public FirebaseConfig()
    {
        AB_Revive = true;
        AB_Offer = false;
        AB_ZoomCamera = false;
        AB_Matching = false;

        AdsInterval = 20;
        AdsFirstShow = 1;

        AppId = "1630865344";
        EnableRating = false;

        sessionTutorial = new SessionTutorial()
        {
            FishId = new List<string>() {
                "Gladiator"
            },
            BladeId = new List<string>()
            {
                "Gladiator"
            }
        };

        offerInfo = new OfferInfo() {
            sessions = new List<int>() {
                5, 9, 12, 15
            },
            stepOutSession = 4
        };

        min_value_revenue = 1;
        config_max_day_send_revenue = 1;

        GamePlayMinScaleSpeed = 0.6f;
        GamePlayMaxScaleSpeed = 1f;
        GamePlayStepScaleSpeed = 0.1f;
        GamePlayKillIncreaseSpeed = 6;

        character_damage = 35;
        character_dash_speed = 20;
        character_hp_cap = 200;
        character_max_health = 100;
        character_max_scale = 2.5f;
        character_move_speed = 10;
        character_powerstrike_damage = 55;


        banner_ads_settings = new BannerAdsSettings() {
            home = true,
            home_settings =  true,
            match_making = true,
            gameplay_settings = true,
            endgame = true
        };

        collectable_data = new CollectableData()  {
            drop_amount = new List<DropAmount>() {
                new DropAmount() { level = 1, small_hp_drop = 0, big_hp_drop = 0, apple_drop = 2, bread_drop = 0, ham_drop = 0},
                new DropAmount() { level = 2, small_hp_drop = 0, big_hp_drop = 0, apple_drop = 3, bread_drop = 0, ham_drop = 0},
                new DropAmount() { level = 3, small_hp_drop = 0, big_hp_drop = 0, apple_drop = 0, bread_drop = 2, ham_drop = 0},
                new DropAmount() { level = 4, small_hp_drop = 0, big_hp_drop = 0, apple_drop = 0, bread_drop = 0, ham_drop = 1}
            },
            small_hp_restore = 20,
            small_max_hp_added = 0,
            big_hp_restore = 50,
            big_max_hp_added = 20,
            apple_energy_added = 0.15f,
            apple_hp_restore = 10,
            bread_energy_added = 0.4f,
            bread_hp_restore = 20,
            ham_energy_added = 1,
            ham_hp_restore = 30
        };

        energy_data = new EnergyData() {
            random_spawn_count = 30,
            energy_added_on_attack = 0.1f,
            energy_added_when_being_hit = 0
        };

        hero_datas = new HeroRemoteDatas() {
            heroes = new List<HeroRemoteData>() {
                new HeroRemoteData() {id = "Gladiator", hp = 100, strike_damage = 30, dash_damage = 50, speed = 10, dash_cooldown = 2},
                new HeroRemoteData() {id = "DarkKnight", hp = 95, strike_damage = 25, dash_damage = 40, speed = 9, dash_cooldown = 1.5f},
                new HeroRemoteData() {id = "Valkyrie", hp = 100, strike_damage = 20, dash_damage = 50, speed = 12.5f, dash_cooldown = 2},
                new HeroRemoteData() {id = "Golem", hp = 120, strike_damage = 30, dash_damage = 35, speed = 8, dash_cooldown = 2},
                new HeroRemoteData() {id = "Goblin", hp = 90, strike_damage = 30, dash_damage = 35, speed = 12, dash_cooldown = 1},
                new HeroRemoteData() {id = "Witch", hp = 80, strike_damage = 35, dash_damage = 45, speed = 10.5f, dash_cooldown = 1},
                new HeroRemoteData() {id = "SkullKing", hp = 110, strike_damage = 30, dash_damage = 40, speed = 10, dash_cooldown = 1.5f},
                new HeroRemoteData() {id = "Devil", hp = 90, strike_damage = 40, dash_damage = 50, speed = 10, dash_cooldown = 0.75f},
            }
        };

        input_disable_dash_strike = 0;
        knock_back_data = new KnockBackData() {
            distance = 9,
            duration = 0.25f
        };

        level_up_datas = new LevelUpDatas() {
            data = new List<LevelUpData>() {
                new LevelUpData() {levelID = 1, kill_required = 0, additional_damage = 10, scale = 1, speed = 1, strike_zone = 1, additional_energy = 1},
                new LevelUpData() {levelID = 2, kill_required = 1, additional_damage = 10, scale = 1.3f, speed = 1.3f, strike_zone = 1.2f, additional_energy = 1},
                new LevelUpData() {levelID = 3, kill_required = 2, additional_damage = 10, scale = 1.4f, speed = 1.5f, strike_zone = 1.5f, additional_energy = 1},
                new LevelUpData() {levelID = 4, kill_required = 3, additional_damage = 10, scale = 1.5f, speed = 1.7f, strike_zone = 2, additional_energy = 1},
            }
        };

        mapConfigs = new MapConfigs() {
            records = new List<MapConfig>() {
                new MapConfig() {id = "map_1", winRequired = 0, mapAiConfig = new List<MapAiConfig>() {new MapAiConfig() {id = "Low", count = 10}, new MapAiConfig(){id = "Medium", count = 0}, new MapAiConfig(){id = "High", count = 0}}},
                new MapConfig() {id = "map_2", winRequired = 1, mapAiConfig = new List<MapAiConfig>() {new MapAiConfig() {id = "Low", count = 7}, new MapAiConfig(){id = "Medium", count = 3}, new MapAiConfig(){id = "High", count = 0}}},
                new MapConfig() {id = "map_3", winRequired = 3, mapAiConfig = new List<MapAiConfig>() {new MapAiConfig() {id = "Low", count = 5}, new MapAiConfig(){id = "Medium", count = 5}, new MapAiConfig(){id = "High", count = 0}}},
                new MapConfig() {id = "map_4", winRequired = 4, mapAiConfig = new List<MapAiConfig>() {new MapAiConfig() {id = "Low", count = 7}, new MapAiConfig(){id = "Medium", count = 2}, new MapAiConfig(){id = "High", count = 1}}},
                new MapConfig() {id = "map_5", winRequired = 5, mapAiConfig = new List<MapAiConfig>() {new MapAiConfig() {id = "Low", count = 6}, new MapAiConfig(){id = "Medium", count = 2}, new MapAiConfig(){id = "High", count = 2}}},
                new MapConfig() {id = "map_6", winRequired = 7, mapAiConfig = new List<MapAiConfig>() {new MapAiConfig() {id = "Low", count = 4}, new MapAiConfig(){id = "Medium", count = 3}, new MapAiConfig(){id = "High", count = 3}}},
            }
        };

        power_strike_hold_time = 0;
        respawn_datas = new RespawnDatas() {
            records = new List<RespawnData>() {
                new RespawnData() {time = 60, respawn_threshold = 50},
                new RespawnData() {time = 120, respawn_threshold = 30},
                new RespawnData() {time = 240, respawn_threshold = 20},
                new RespawnData() {time = 300, respawn_threshold = 0},
                new RespawnData() {time = 360, respawn_threshold = 0}
            }
        };

        scale_per_kill = 0.05f;
        slash_data = new SlashData() {
            range = 7, speed = 6, damagePercentage = 80, radius = 2
        };

        strike_cooldown = 0.35f;
    }

    public LevelUpData GetLevelUpDataByKill(int kill)
    {
        for (int i = level_up_datas.data.Count - 1; i >= 0; i--)
        {
            if (kill >= level_up_datas.data[i].kill_required)
            {
                return level_up_datas.data[i];
            }
        }

        return null;
    }
}

[Serializable]
public class OfferInfo
{
    public List<int> sessions;
    public int stepOutSession;
}

[Serializable]
public class SessionTutorial
{
    public List<string> FishId;
    public List<string> BladeId;
}

[Serializable]
public class LevelUpData
{
    public int levelID { get; set; }
    public int kill_required { get; set; }
    public int additional_damage { get; set; }
    public float scale { get; set; }
    public float speed { get; set; }
    public float strike_zone { get; set; }
    public float additional_energy { get; set; }
}

[Serializable]
public class LevelUpDatas
{
    public List<LevelUpData> data;
}

[Serializable]
public class DropAmount
{
    public int level { get; set; }
    public int small_hp_drop { get; set; }
    public int big_hp_drop { get; set; }
    public int apple_drop { get; set; }
    public int bread_drop { get; set; }
    public int ham_drop { get; set; }
}

[Serializable]
public class CollectableData
{
    public List<DropAmount> drop_amount { get; set; }
    public int small_hp_restore { get; set; }
    public int small_max_hp_added { get; set; }
    public int big_hp_restore { get; set; }
    public int big_max_hp_added { get; set; }
    public float apple_energy_added { get; set; }
    public float apple_hp_restore { get; set; }
    public float bread_energy_added { get; set; }
    public float bread_hp_restore { get; set; }
    public float ham_energy_added { get; set; }
    public float ham_hp_restore { get; set; }
}

[Serializable]
public class KnockBackData
{
    public float distance { get; set; }
    public float duration { get; set; }
}

[Serializable]
public class HeroRemoteData
{
    public string id { get; set; }
    public float hp { get; set; }
    public float strike_damage { get; set; }
    public float dash_damage { get; set; }
    public float speed { get; set; }
    public float dash_cooldown { get; set; }
}

[Serializable]
public class HeroRemoteDatas
{
    public List<HeroRemoteData> heroes { get; set; }
}

[Serializable]
public class MapAiConfig
{
    public string id { get; set; }
    public int count { get; set; }
}

[Serializable]
public class MapConfig
{
    public string id { get; set; }
    public int winRequired { get; set; }
    public List<MapAiConfig> mapAiConfig { get; set; }
}

[Serializable]
public class MapConfigs
{
    public List<MapConfig> records { get; set; }
}

[Serializable]
public class BannerAdsSettings
{
    public bool home { get; set; }
    public bool home_settings { get; set; }
    public bool match_making { get; set; }
    public bool gameplay_settings { get; set; }
    public bool endgame { get; set; }
}

[Serializable]
public class RespawnData
{
    public float time { get; set; }
    public float respawn_threshold { get; set; }
}

[Serializable]
public class RespawnDatas
{
    public List<RespawnData> records { get; set; }
}

[Serializable]
public class EnergyData
{
    public int random_spawn_count { get; set; }
    public float energy_added_on_attack { get; set; }
    public float energy_added_when_being_hit { get; set; }
}

[Serializable]
public class SlashData
{
    public float range { get; set; }
    public float speed { get; set; }
    public float damagePercentage { get; set; }
    public float radius { get; set; }
}