using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManaBar : MonoBehaviour
{
    [SerializeField] RectTransform scaleRoot;
    [SerializeField] Text manaTxt;

    public void SetManaBarVisual(float percentage)
    {
        percentage = Mathf.Clamp01(percentage);

        scaleRoot.localScale = new Vector3(percentage, 1, 1);
    }

    public void SetManaTxt(int _mana)
    {
        manaTxt.text = _mana.ToString();
    }
}
