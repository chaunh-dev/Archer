using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsConfigHelper : MonoBehaviour
{
    private List<CharacterStatsConfigData> datalist;
    private List<CharacterStatsConfigData> GetConfig()
    {
        InitDataList();

        return datalist;
    }

    public void InitDataList()
    {
        if (datalist == null)
        {
            datalist = new List<CharacterStatsConfigData>();
            for (int i = 0; i < ConfigManager.instance.GetConfig<CharacterStatsConfigCollection>().Count; i++)
            {
                CharacterStatsConfigData data = new CharacterStatsConfigData();
                data = ConfigManager.instance.GetConfig<CharacterStatsConfigCollection>()[i];
                datalist.Add(data);
            }
        }
    }

    public int GetDefaultHP()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "HPIncrease").Default;
    }

    public int GetHPIncrease()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "HPIncrease").StatIncrease;
    }

    public int GetDefaultMP()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "ManaIncrease").Default;
    }

    public int GetMPIncrease()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "ManaIncrease").StatIncrease;
    }

    public int GetDefaultHPRegen()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "HealthRegen").Default;
    }

    public int GetHPRegenIncrease()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "HealthRegen").StatIncrease;
    }

    public int GetDefautlMPRegen()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "ManaRegen").Default;
    }

    public int GetMPRegenIncrease()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "ManaRegen").StatIncrease;
    }

    public int GetDefaultCriticalChance()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "CriticalChance").Default;
    }

    public int GetCritcalChanceIncrease()
    {
        return GetConfig().Find(x => x.Stats.Trim() == "CriticalChance").StatIncrease;
    }
}
