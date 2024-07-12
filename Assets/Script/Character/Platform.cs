using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] List<Tile> tiles = new List<Tile>();
    [SerializeField] private GameObject archer;
    public Action onFallOff;

    void Start()
    {
        tiles.AddRange(GetComponentsInChildren<Tile>());
    }

    public void Release()
    {
        foreach (Tile tile in tiles)
        {
            tile.Release();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject == archer)
        {
            onFallOff?.Invoke();
        }
    }

    public void AddEventOnFallOff(Action evt)
    {
        onFallOff += evt;
    }
}
