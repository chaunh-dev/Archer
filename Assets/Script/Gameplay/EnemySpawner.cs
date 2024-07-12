using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPositions;
    [SerializeField] List<Transform> spawnPosForVerticalBot;
    [SerializeField] List<Transform> spawnPosForHorizontalBot;
    [SerializeField] List<GameObject> enemyVariants;
    [SerializeField] GameObject boss;
    private List<Vector3> availablePositions = new List<Vector3>();

    public Archer SpawnRandomEnemy()
    {
        if (spawnPositions.Count == 0 || enemyVariants.Count == 0)
        {
            Debug.LogError("Spawn positions or enemy variants not set in EnemySpawner.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
        Vector3 position = availablePositions[randomIndex];
        availablePositions.RemoveAt(randomIndex);

        GameObject enemyVariant = enemyVariants.RandomElement();

        GameObject go = Instantiate(enemyVariant, position, Quaternion.identity, null);

        return go.GetComponentInChildren<Archer>();
    }

    public void ResetAvailablePositions()
    {
        availablePositions.Clear();
        foreach (Transform spawnPosition in spawnPositions)
        {
            availablePositions.Add(spawnPosition.position);
        }
    }

    public Archer SpawnPatrolBot(BotPatrol.PatrolType patrolType)
    {
        GameObject enemyVariant = enemyVariants.RandomElement();
        Transform spawnPos = null;

        if (patrolType == BotPatrol.PatrolType.Vertical)
        {
            spawnPos = spawnPosForVerticalBot.RandomElement();
        }
        else if (patrolType == BotPatrol.PatrolType.Horizontal)
        {
            spawnPos = spawnPosForHorizontalBot.RandomElement();
        }

        if (spawnPos == null)
        {
            Debug.LogError("Spawn position is not set. Please check the spawn positions configuration.");
            return null;
        }

        GameObject go = Instantiate(enemyVariant, spawnPos.position, Quaternion.identity, null);
        go.GetComponentInChildren<BotPatrol>().SetPatrolType(patrolType);
        go.GetComponentInChildren<BotPatrol>().IsPatrolBot(true);

        return go.GetComponentInChildren<Archer>();
    }

    public Archer SpawnBlinkBot()
    {
        GameObject enemyVariant = enemyVariants.RandomElement();
        Vector3 tempPos = new Vector3(0, 0, 0);
        GameObject go = Instantiate(enemyVariant, tempPos, Quaternion.identity, null);
        go.GetComponentInChildren<BlinkBot>().IsBlinkBot(true);
        go.GetComponentInChildren<BlinkBot>().Init(spawnPositions);

        return go.GetComponentInChildren<Archer>();
    }

    public Archer SpawnBoss()
    {
        int randomIndex = UnityEngine.Random.Range(0, availablePositions.Count);
        Vector3 position = availablePositions[randomIndex];
        availablePositions.RemoveAt(randomIndex);

        GameObject go = Instantiate(boss, position, Quaternion.identity, null);

        return go.GetComponentInChildren<Archer>();

    }
}

[Serializable]
public class Bot
{
    public BotType botType;
    public enum BotType
    {
        singleBot,
        doubleBot,
        trippleBot,
        verticalMovingBot,
        horizontalMovingBot,
        blinkBot
    }
}
