using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] private int totalWave = 0;
    [SerializeField] private List<Archer> enemyList;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private LevelBase currentLevel;
    [SerializeField] private Archer player;
    [SerializeField] private int waveKillBots;
    [SerializeField] HeadShot headShot;
    [SerializeField] int numberOfAimPoint;
    private int currentWave = 0;
    private float timeLeft;
    public static LevelController Instance;
    private float regenerationTimePeriod = 5f;
    private float timer = 0f;
    public int Level { get => level; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InitLevel();
    }

    private void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else timeLeft = 0;

        timer += Time.deltaTime;

        if (timer >= regenerationTimePeriod)
        {
            if (player.IsDead()) return;
            player.HealthRegen(1);
            // Reset the timer
            timer = 0f;
        }
    }

    public void InitLevel()
    {
        enemyList.Clear();
        currentWave = 0;
        currentLevel = GameConfigManager.instance.level.GetLevelData(level - 1);
        timeLeft = currentLevel.time;
        totalWave = currentLevel.wave;
        Gameplay.Instance.SetWave(totalWave);
        enemySpawner.ResetAvailablePositions();
        SpawnRandomEnemyNextWave();
        player.GetComponent<BowAimControl>().SetNumberOfPoint(numberOfAimPoint);
    }

    public void SetNextLevel(int _level)
    {
        if (_level != 0 && _level <= GameConfigManager.instance.level.GetHighestLevel())
        {
            level = _level;
            UserDataService.instance.UserData.Level = level;
            UserDataService.instance.Save(true);
        }
    }

    private void EndGameSequence()
    {
        EndGame();
    }

    public void EndGame()
    {
        // foreach (Archer enemy in enemyList)
        // {
        //     if (enemy == null) continue;
        //     enemy.transform.parent.gameObject.SetActive(false);
        // }

        // if (player != null)
        //     player.transform.gameObject.SetActive(false);

        level++;
        InitLevel();
    }

    public void SpawnRandomEnemyNextWave()
    {
        if (currentWave >= totalWave) return;

        if (currentLevel.singleBot != 0)
        {
            waveKillBots = 1;
            SpawnEnemy(player.gameObject, Bot.BotType.singleBot);
            currentLevel.singleBot--;
            return;
        }

        if (currentLevel.doubleBot != 0)
        {
            waveKillBots = 2;
            SpawnEnemy(player.gameObject, Bot.BotType.doubleBot);
            currentLevel.doubleBot--;
            return;
        }

        if (currentLevel.trippleBot != 0)
        {
            waveKillBots = 3;
            SpawnEnemy(player.gameObject, Bot.BotType.trippleBot);
            currentLevel.trippleBot--;
            return;
        }

        if (currentLevel.verticalMovingBot != 0)
        {
            waveKillBots = 1;
            SpawnEnemy(player.gameObject, Bot.BotType.verticalMovingBot);
            currentLevel.verticalMovingBot--;
            return;
        }

        if (currentLevel.horizontalMovingBot != 0)
        {
            waveKillBots = 1;
            SpawnEnemy(player.gameObject, Bot.BotType.horizontalMovingBot);
            currentLevel.horizontalMovingBot--;
            return;
        }

        if (currentLevel.blinkBot != 0)
        {
            waveKillBots = 1;
            SpawnEnemy(player.gameObject, Bot.BotType.blinkBot);
            currentLevel.blinkBot--;
            return;
        }

        if (currentWave == totalWave - 1)
        {
            waveKillBots = 1;
            Archer boss = enemySpawner.SpawnBoss();
            boss.SetUpHealthBar(GameConfigManager.instance.level.GetBossHealth());
            boss.GetComponent<AutoAimTarget>().SetTarget(player.gameObject.transform);
            enemyList.Add(boss);
            return;
        }
    }

    private void SpawnEnemy(GameObject target, Bot.BotType botType)
    {
        switch (botType)
        {
            case Bot.BotType.singleBot:
                Archer singleEnemy = enemySpawner.SpawnRandomEnemy();
                singleEnemy.SetUpHealthBar(GameConfigManager.instance.level.GetBotHealth());
                enemyList.Add(singleEnemy);
                singleEnemy.GetComponent<AutoAimTarget>().SetTarget(target.transform);
                break;
            case Bot.BotType.doubleBot:
                for (int i = 0; i < 2; i++)
                {
                    Archer doubleEnemy = enemySpawner.SpawnRandomEnemy();
                    doubleEnemy.SetUpHealthBar(GameConfigManager.instance.level.GetBotHealth());
                    enemyList.Add(doubleEnemy);
                    doubleEnemy.GetComponent<AutoAimTarget>().SetTarget(target.transform);
                }
                break;
            case Bot.BotType.trippleBot:
                for (int i = 0; i < 3; i++)
                {
                    Archer trippleEnemy = enemySpawner.SpawnRandomEnemy();
                    trippleEnemy.SetUpHealthBar(GameConfigManager.instance.level.GetBotHealth());
                    enemyList.Add(trippleEnemy);
                    trippleEnemy.GetComponent<AutoAimTarget>().SetTarget(target.transform);
                }
                break;
            case Bot.BotType.verticalMovingBot:
                Archer verticalMovEnemy = enemySpawner.SpawnPatrolBot(BotPatrol.PatrolType.Vertical);
                verticalMovEnemy.SetUpHealthBar(GameConfigManager.instance.level.GetBotHealth());
                enemyList.Add(verticalMovEnemy);
                verticalMovEnemy.GetComponent<AutoAimTarget>().SetTarget(target.transform);
                break;
            case Bot.BotType.horizontalMovingBot:
                Archer horizontalMovEnemy = enemySpawner.SpawnPatrolBot(BotPatrol.PatrolType.Horizontal);
                horizontalMovEnemy.SetUpHealthBar(GameConfigManager.instance.level.GetBotHealth());
                enemyList.Add(horizontalMovEnemy);
                horizontalMovEnemy.GetComponent<AutoAimTarget>().SetTarget(target.transform);
                break;
            case Bot.BotType.blinkBot:
                Archer blinkEnemy = enemySpawner.SpawnBlinkBot();
                blinkEnemy.SetUpHealthBar(GameConfigManager.instance.level.GetBotHealth());
                enemyList.Add(blinkEnemy);
                blinkEnemy.GetComponent<AutoAimTarget>().SetTarget(target.transform);
                break;
        }
    }

    public LevelBase CurrentLevelData()
    {
        return currentLevel;
    }

    public void NextWave()
    {
        waveKillBots--;

        if (waveKillBots != 0) return;

        if (currentWave < totalWave)
            Gameplay.Instance.ClearWave(currentWave);

        if (currentWave == totalWave - 1)
        {
            Gameplay.Instance.ClearWave(currentWave, true);
            level++;
            return;
        }

        currentWave++;

        enemySpawner.ResetAvailablePositions();
        SpawnRandomEnemyNextWave();
    }

    public void OnPause(bool _status)
    {
        Time.timeScale = _status ? 0 : 1;
    }

    public void OnHeadShot(Transform _target)
    {
        headShot.EffOn(_target);
    }

    public void PlayerHealthRegen(int _healthPoint)
    {
        player.HealthRegen(_healthPoint, false);
    }
}
