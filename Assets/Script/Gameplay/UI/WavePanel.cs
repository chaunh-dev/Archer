using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavePanel : MonoBehaviour
{
    [SerializeField] private List<WaveSkeleton> skeletons = new List<WaveSkeleton>();
    [SerializeField] private GameObject WaveSkel;

    public void Init(int totalWave)
    {
        Refresh();
        for (int i = 0; i < totalWave; i++)
        {
            GameObject _waveSkelObj = Instantiate(WaveSkel, transform);
            WaveSkeleton _waveSkel = _waveSkelObj.GetComponent<WaveSkeleton>();
            if (i < totalWave - 1)
            {
                int temp = Random.Range(4, 6);
                _waveSkel.SetAnimationByIndex(temp, true);
            }
            else
                _waveSkel.SetAnimationByName(WaveSkullAnim.Skull_Boss_Idle, true);

            skeletons.Add(_waveSkel);
        }
    }

    private void Refresh()
    {
        foreach (WaveSkeleton waveSkeleton in skeletons)
        {
            Destroy(waveSkeleton.gameObject);
        }
        skeletons.Clear();
    }

    public void ChangeAnimationByName(int _index, WaveSkullAnim animEnum, bool isLoop)
    {
        skeletons[_index].SetAnimationByName(animEnum, isLoop);
    }

    public void ChangeAnimationByIndex(int _index, int _indexAnim, bool _isLoop)
    {
        skeletons[_index].SetAnimationByIndex(_indexAnim, _isLoop);
    }
}
