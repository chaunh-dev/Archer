using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBot : MonoBehaviour
{
    [SerializeField] private float teleDelayTime;
    [SerializeField] List<Transform> spawnList = new List<Transform>();
    [SerializeField] private bool isBlinkBot = false;
    private List<Vector3> availablePositions = new List<Vector3>();
    private float timer;
    private float periodTime = 5f;

    private void Awake()
    {
        isBlinkBot = false;
    }

    private void Update()
    {
        if (!isBlinkBot)
            return;

        if (spawnList == null)
            Debug.LogError("Spawn spots were not set.");

        timer += Time.deltaTime;
        if (timer >= periodTime * teleDelayTime)
        {
            Teleport();
            timer = 0;
        }
    }

    private void Teleport()
    {
        if (availablePositions.Count == 0)
            ResetAvailablePositions();
        int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
        Vector3 _position = availablePositions[randomIndex];
        availablePositions.RemoveAt(randomIndex);
        this.transform.position = _position;
    }

    private void ResetAvailablePositions()
    {
        availablePositions.Clear();
        foreach (Transform spawnPosition in spawnList)
        {
            availablePositions.Add(spawnPosition.position);
        }
    }

    public void IsBlinkBot(bool _isBlinkBot)
    {
        this.isBlinkBot = _isBlinkBot;
    }

    public void Init(List<Transform> _list)
    {
        spawnList = _list;
    }
}
