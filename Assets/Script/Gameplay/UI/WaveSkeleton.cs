using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
using UnityEngine;

public class WaveSkeleton : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation skeletonAnimation;

    public void SetAnimationByIndex(int index, bool loop)
    {
        if (index >= 0 && index < skeletonAnimation.Skeleton.Data.Animations.Count)
        {
            skeletonAnimation.state.SetAnimation(0, skeletonAnimation.Skeleton.Data.Animations.Items[index], loop);
        }
        else
        {
            Debug.LogWarning("Animation index out of bounds");
        }
    }

    public void SetAnimationByName(WaveSkullAnim animEnum, bool _isLoop)
    {
        skeletonAnimation.state.SetAnimation(0, GetAnimName(animEnum), _isLoop);
    }

    public string GetAnimName(WaveSkullAnim animEnum)
    {
        switch (animEnum)
        {
            case WaveSkullAnim.Skull_Boss_Die:
                return "Skull_Boss-Die";
            case WaveSkullAnim.Skull_Boss_Idle:
                return "Skull_Boss-Idle";
            case WaveSkullAnim.Skull_Normal_Idle:
                return "Skull_Normal-Idle";
            case WaveSkullAnim.Skull_Normal_Die:
                return "Skull_Normal-Die";
            case WaveSkullAnim.Skull_Normal_Smile:
                return "Skull_Normal-Smill";
            default:
                return string.Empty;
        }
    }
}

public enum WaveSkullAnim
{
    Skull_Boss_Die,
    Skull_Boss_Idle,
    Skull_Normal_Idle,
    Skull_Normal_Die,
    Skull_Normal_Smile,
}
