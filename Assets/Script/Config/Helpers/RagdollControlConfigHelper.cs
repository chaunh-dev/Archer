using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollControlConfigHelper : MonoBehaviour
{
    private RagdollControlConfigData GetConfig()
    {
        return ConfigManager.instance.GetConfig<RagdollControlConfigCollection>()[0];
    }

    public float GetBowControlAcceleration()
    {
        return GetConfig().bowControlAcceleration;
    }

    public float GetBowControlDampening()
    {
        return GetConfig().bowControlDampening;
    }

    public float GetBowControlBaseVelocity()
    {
        return GetConfig().bowControlBaseVelocity;
    }

    public int GetMinFireForce()
    {
        return GetConfig().fireForceMin;
    }

    public int GetMaxFireForce()
    {
        return GetConfig().fireForceMax;
    }

    public float GetBowPullDuration()
    {
        return GetConfig().arrowPullDuration;
    }

    public float GetArrowDrag()
    {
        return GetConfig().arrowDrag;
    }
}
