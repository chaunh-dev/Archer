using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public static Gameplay Instance;
    [SerializeField] private UIGameplay uIGameplay;

    private void Awake()
    {
        Instance = this;
    }

    public bool IsWebGLFlow
    {
        get
        {
#if UNITY_WEBGL
            return true;
#else
            return false;
#endif
        }
    }

    public void SetWave(int _totalWave)
    {
        uIGameplay.wavePanelInit(_totalWave);
    }

    public void ClearWave(int _currentWave, bool isFinalWave = false)
    {
        if (!isFinalWave)
        {
            uIGameplay.WaveClear(_currentWave);
            return;
        }

        uIGameplay.FinalWaveClear(_currentWave);
        LevelController.Instance.EndGame();
    }
}
