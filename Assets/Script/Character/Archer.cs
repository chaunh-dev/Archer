using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Archer : MonoBehaviour
{
    [SerializeField] CharacterHealth health;
    [SerializeField] RagdollControl ragDollControl;
    [SerializeField] BowAimControl bowAimControl;
    [SerializeField] Platform platform;
    [SerializeField] private bool isHelmetProtected;
    [SerializeField] private bool isChestArmorProtected;
    [SerializeField] private bool isLegsArmorProtected;
    public bool isBot;
    private bool isNextWave = false;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        if (health.IsDead() && isBot) Release();
        if (health.IsFull()) ragDollControl.BodyColorReset();
    }

    public void Init(bool isRespawn = false, int currentHP = 0)
    {
        ragDollControl.AssignBodyPartPiercedCallbacks(OnArrowPierced);
        isBot = bowAimControl == null ? true : false;

        if (!isBot)
        {
            SetUpHealthBar(GameConfigManager.instance.stats.GetDefaultHP());
        }
        else
        {
            platform = transform.parent.GetComponentInChildren<Platform>();
            platform.AddEventOnFallOff(() => Release());
        }
    }

    public void SetUpHealthBar(int _healthPoint)
    {
        health.Init(_healthPoint);
    }

    public void SetCurrHP(int _hp)
    {
        health.Amount = _hp;
    }

    public int GetCurrHP()
    {
        return health.Amount;
    }

    public void HealthRegen(int _heathPoint, bool revise = false)
    {
        if (health.IsFull()) return;
        health.HealthRegen(_heathPoint, revise);
    }

    public void TakeDamage(int _damage)
    {
        health.TakeDamage(_damage);
        if (health.IsDead() && isBot)
        {
            Release();
        }
    }

    public bool IsDead()
    {
        return health.IsDead();
    }

    public void BodyInjured()
    {
        ragDollControl.InjuredBody();
    }

    private void OnArrowPierced(BodyPart part, Arrow arrow)
    {
        part.Injured();

        int _damageTook = 0;
        switch (part.GetBodyType())
        {
            case BodyPartType.Head:
                _damageTook = GameConfigManager.instance.arrowStats.GetDamageToBodyPart(part.GetBodyType(), arrow);
                break;
            case BodyPartType.Chest:
            case BodyPartType.Arm:
                _damageTook = GameConfigManager.instance.arrowStats.GetDamageToBodyPart(part.GetBodyType(), arrow);
                break;
            case BodyPartType.Leg:
                _damageTook = GameConfigManager.instance.arrowStats.GetDamageToBodyPart(part.GetBodyType(), arrow);
                break;
        }

        health.TakeDamage(_damageTook);

        if (health.IsDead() && isBot)
        {
            Release();
        }
        else
        {
            float desiredForce =
                GameConfigManager.instance.arrowStats.GetForceToBodyPart(part.GetBodyType(), arrow);
            part.GetComponent<Rigidbody>().AddForce(arrow.rb.velocity * desiredForce, ForceMode.Impulse);
        }
    }

    private void Release()
    {
        if (isNextWave) return;
        isNextWave = true;

        ragDollControl.ReleaseRagdoll();

        platform.Release();
        if (isBot)
        {
            StartCoroutine(IDestroyArcher(() =>
            {
                LevelController.Instance.NextWave();
            }));
        }
    }

    public int GetMaxHealth()
    {
        return health.MaxHealth;
    }

    private IEnumerator IDestroyArcher(Action archerDead = null)
    {
        yield return new WaitForSeconds(3f);
        archerDead?.Invoke();
        yield return new WaitForSeconds(3f);
        Destroy(transform.parent.gameObject);
    }
}
