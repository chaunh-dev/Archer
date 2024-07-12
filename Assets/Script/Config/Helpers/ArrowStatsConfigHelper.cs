using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowStatsConfigHelper : MonoBehaviour
{
    private List<ArrowStatsConfigData> dataList;
    private List<ArrowStatsConfigData> GetConfig()
    {
        if (dataList == null)
        {
            dataList = new List<ArrowStatsConfigData>();
            for (int i = 0; i < ConfigManager.instance.GetConfig<ArrowStatsConfigCollection>().Count; i++)
            {
                ArrowStatsConfigData data = new ArrowStatsConfigData();
                data = ConfigManager.instance.GetConfig<ArrowStatsConfigCollection>()[i];
                dataList.Add(data);
            }
        }

        return dataList;
    }

    public int GetDamageToBodyPart(BodyPartType type, Arrow arrow)
    {
        ArrowStatsConfigData config = GetConfig().ElementAt(0);
        if (config == null) config = dataList[0];
        switch (type)
        {
            case BodyPartType.Head:
                return config.damageToHead;
            case BodyPartType.Arm:
                return config.damageToArm;
            case BodyPartType.Leg:
                return config.damageToLeg;
            case BodyPartType.Chest:
                return config.damageToBody;
            default:
                return config.damageToNeck;

        }
    }

    public int GetForceToBodyPart(BodyPartType type, Arrow arrow)
    {
        ArrowStatsConfigData config = GetConfig().ElementAt(0);
        switch (type)
        {
            case BodyPartType.Head:
                return config.forceToHead;
            case BodyPartType.Arm:
                return config.forceToArm;
            case BodyPartType.Leg:
                return config.forceToLeg;
            case BodyPartType.Chest:
                return config.forceToBody;
            default:
                return config.forceToNeck;

        }
    }
}
