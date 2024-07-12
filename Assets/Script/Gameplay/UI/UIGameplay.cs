using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private Text levelTxt;
    [SerializeField] private WavePanel wavePanel;
    [SerializeField] Sprite[] backgrounds;
    [SerializeField] SpriteRenderer background;
    [SerializeField] GameObject greenScreen;
    [SerializeField] Button cheatBtn;
    public CharacterHealthBar healthBar;

    private void Start()
    {
        LoadBG();
    }

    public void LoadBG(Sprite _sprite = null)
    {
        if (_sprite != null)
        {
            background.sprite = _sprite;
            return;
        }
        if (backgrounds.Length > 1)
        {
            int index = Random.Range(0, backgrounds.Length);
            background.sprite = backgrounds[index];
        }
    }

    public void wavePanelInit(int _totalWave)
    {
        wavePanel.Init(_totalWave);
    }

    public void WaveClear(int _currentWave)
    {
        wavePanel.ChangeAnimationByName(_currentWave, WaveSkullAnim.Skull_Normal_Die, false);
    }

    public void FinalWaveClear(int _currentWave)
    {
        wavePanel.ChangeAnimationByName(_currentWave, WaveSkullAnim.Skull_Boss_Die, false);
    }

    public void SetLevelText(string level)
    {
        levelTxt.text = level;
    }
}
