using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthBar : MonoBehaviour
{
    [SerializeField] RectTransform scaleRoot;
    [SerializeField] Text healthTxt;

    public void SetHealthBarVisual(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);

        scaleRoot.localScale = new Vector3(percentage, 1, 1);
    }

    public void SetHealthTxt(int _health)
    {
        if (healthTxt == null) return;
        healthTxt.text = _health.ToString();
    }
}
