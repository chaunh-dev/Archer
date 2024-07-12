using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class AutoAimTarget : MonoBehaviour
{
    [Header("BOT")]
    [SerializeField] BowAimControl bowAimControl;
    [SerializeField] BowFire bowFire;
    [SerializeField] float shotInterval;
    [SerializeField] float aimDuration;
    [SerializeField] float aimErrorness;
    [SerializeField] float accurateChance;
    [SerializeField] float headHitChance;
    [SerializeField] float bodyHitChance;
    [SerializeField] float legsHitChance;
    [SerializeField] string aI_ID;
    [SerializeField] bool isBoss;

    [Header("Player")]
    [SerializeField] private bool isPlayer;
    public event Action OnGiftReceived;

    private List<Transform> aimPostion = new List<Transform>();
    private List<TargetAim> aimTarget = new List<TargetAim>();
    private List<float> aimPosPercent = new List<float>();
    private float shotIntervalTimer;
    private float aimDurationTimer;
    private Transform target;
    private float timer = 0;

    public bool IsBoss { get => isBoss; }

    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (aimTarget == null)
        {
            return;
        }

        if (!isPlayer)
        {
            if (gameObject.GetComponent<Archer>().IsDead())
                return;

            timer += Time.deltaTime;
            bowFire.UpdateStringByPercent(timer, shotInterval);

            if (aimPostion == null) return;

            if (shotIntervalTimer <= 0)
            {
                bowFire.PutArrowOnBow();
                bowAimControl.AimToPosition(CalculateAimPosition());
                bowAimControl.SetBaseVelocity();
                shotIntervalTimer = shotInterval;
                aimDurationTimer = aimDuration;
                timer = 0;
            }

            if (aimDurationTimer <= 0 && bowFire.IsHoldingArrow())
            {
                bowFire.FireArrow();
            }

            shotIntervalTimer -= Time.deltaTime;
            aimDurationTimer -= Time.deltaTime;
        }
    }

    private void Init()
    {
        if (IsBoss)
            aI_ID = GameConfigManager.instance.level.GetBossAIProfile();
        else
            aI_ID = GameConfigManager.instance.level.GetBotAIProfile();
        // shotInterval = GameConfigManager.instance.bot.GetShotInterval();
        aimDuration = GameConfigManager.instance.aIProfile.GetWaitUntilShoot(aI_ID);
        aimErrorness = GameConfigManager.instance.bot.GetAimErrorness();
        accurateChance = GameConfigManager.instance.aIProfile.GetAccurateChance(aI_ID) / 100;
        if (LevelController.Instance.Level < 11)
            headHitChance = 0;
        else
            headHitChance = GameConfigManager.instance.aIProfile.GetHeadHitChance(aI_ID) / 100;
        legsHitChance = GameConfigManager.instance.aIProfile.GetLegsHitChance(aI_ID) / 100;
        bodyHitChance = GameConfigManager.instance.aIProfile.GetBodyHitChance(aI_ID) / 100;
        aimPosPercent.Add(bodyHitChance);
        aimPosPercent.Add(legsHitChance);
        aimPosPercent.Add(headHitChance);
        shotInterval = aimDuration + 0.5f;
        shotIntervalTimer = 0;
        aimDurationTimer = aimDuration;
    }

    public void SetTarget(Transform _target)
    {
        aimTarget.Clear();
        aimPostion.Clear();
        // aimTarget = _target;
        this.target = _target;
        if (aimTarget != null)
        {
            aimTarget.AddRange(target.GetComponentsInChildren<TargetAim>());
            foreach (TargetAim tarPos in aimTarget)
            {
                aimPostion.Add(tarPos.gameObject.transform);
            }

        }
    }

    private Vector3 CalculateAimPosition()
    {
        Vector3 aimPos = SelectRandomByWeight(aimPostion, aimPosPercent).position;
        aimPos += new Vector3(0, -1f, 0);
        bool isHit = Random.Range(0.0f, 1.0f) < accurateChance;
        if (!isHit)
        {
            float tempRandErrorness = Random.Range(0, 2) == 1 ? aimErrorness * 4 : -aimErrorness * 4;
            aimPos.y += tempRandErrorness;
        }

        return aimPos;
    }

    private Transform SelectRandomByWeight(List<Transform> aimPos, List<float> weights)
    {
        float cumulativeWeight = 0;
        float randomValue = Random.Range(0.0f, 1.0f);

        for (int i = 0; i < weights.Count; i++)
        {
            cumulativeWeight += weights[i];
            if (randomValue < cumulativeWeight)
            {
                return aimPos[i];
            }
        }

        // In case of rounding issues, return the last element
        return aimPos[aimPos.Count - 1];
    }
}
