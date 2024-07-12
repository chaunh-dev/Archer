using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayConfigHelper : MonoBehaviour
{
    private GameplayConfigData GetConfig()
    {
        return ConfigManager.instance.GetConfig<GameplayConfigCollection>()[0];
    }

    public int GetCameraSize()
    {
        return GetConfig().cameraSize;
    }
}
