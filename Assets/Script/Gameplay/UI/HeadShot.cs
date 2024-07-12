using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    [SerializeField] GameObject headshotEff;

    public void EffOn(Transform _target)
    {
        Vector3 tempPos = _target.transform.position;
        tempPos.y += 2;
        transform.position = tempPos;
        headshotEff.SetActive(true);
        Invoke(nameof(TurnOff), 1.5f);
    }

    private void TurnOff()
    {
        headshotEff.SetActive(false);
    }
}
