using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDisable : MonoBehaviour
{
    public float TimeDisable = 5f;

    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(CooldownDisable());
    }

    private void OnDisable()
    {
        StopCoroutine(CooldownDisable());
    }

    IEnumerator CooldownDisable()
    {
        yield return new WaitForSeconds(TimeDisable);

        gameObject.SetActive(false);
    }
}
