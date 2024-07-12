using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIProfileConfigHelper : MonoBehaviour
{
    private List<AIProfileConfigData> dataList;
    private List<AIProfileConfigData> GetConfig()
    {
        if (dataList == null)
        {
            dataList = new List<AIProfileConfigData>();
            for (int i = 0; i < ConfigManager.instance.GetConfig<AIProfileConfigCollection>().Count; i++)
            {
                AIProfileConfigData data = new AIProfileConfigData();
                data = ConfigManager.instance.GetConfig<AIProfileConfigCollection>()[i];
                dataList.Add(data);
            }
        }

        return dataList;
    }

    public float GetWaitUntilShoot(string _id)
    {
        return GetConfig().Find(x => x.ID == $"{_id}").waitUntilShoot;
    }

    public float GetAccurateChance(string _id)
    {
        return GetConfig().Find(x => x.ID == $"{_id}").accurateChance;
    }

    public float GetHeadHitChance(string _id)
    {
        return GetConfig().Find(x => x.ID == $"{_id}").headHitChance;
    }

    public float GetBodyHitChance(string _id)
    {
        return GetConfig().Find(x => x.ID == $"{_id}").bodyHitChance;
    }

    public float GetLegsHitChance(string _id)
    {
        return GetConfig().Find(x => x.ID == $"{_id}").legsHitChance;
    }
}
