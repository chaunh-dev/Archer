using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotConfigHelper : MonoBehaviour
{
    private BotConfigData GetConfig()
    {
        return ConfigManager.instance.GetConfig<BotConfigCollection>()[0];
    }

    public float GetShotInterval()
    {
        return GetConfig().shotInterval;
    }

    public float GetAimDurationMin()
    {
        return GetConfig().aimDurationMin;
    }

    public float GetAimDurationMax()
    {
        return GetConfig().aimDurationMax;
    }

    public float GetAimErrorness()
    {
        return GetConfig().aimErrorness;
    }

    public float GetRandomAimDuration()
    {
        return Random.Range(GetAimDurationMin(), GetAimDurationMax());
    }
}
