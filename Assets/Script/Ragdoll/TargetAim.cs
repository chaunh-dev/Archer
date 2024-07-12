using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAim : MonoBehaviour
{
    public Target target;
    [SerializeField] private float hitChance;

    public enum Target
    {
        Head,
        Body,
        Legs,
    }

    public float GetHitChanceOfTheTarget(string _aIID)
    {
        switch (this.target)
        {
            case Target.Head:
                hitChance = GameConfigManager.instance.aIProfile.GetHeadHitChance(_aIID);
                break;
            case Target.Body:
                hitChance = GameConfigManager.instance.aIProfile.GetBodyHitChance(_aIID);
                break;
            case Target.Legs:
                hitChance = GameConfigManager.instance.aIProfile.GetLegsHitChance(_aIID);
                break;
        }

        return hitChance;
    }
}
