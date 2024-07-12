using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum BodyPartType
{
    Head,
    Chest,
    Arm,
    Leg,
}

public class RagdollControl : MonoBehaviour
{
    [SerializeField] private List<BodyPart> bodyParts = new List<BodyPart>();
    [SerializeField] private BodyPartBlood bodyPartBlood;
    private List<FollowObject> followObjects = new List<FollowObject>();

    // Start is called before the first frame update
    private void Start()
    {
        bodyParts.AddRange(GetComponentsInChildren<BodyPart>());
        followObjects.AddRange(GetComponentsInChildren<FollowObject>());
    }

    public void AssignBodyPartPiercedCallbacks(Action<BodyPart, Arrow> func)
    {
        foreach (BodyPart part in bodyParts)
        {
            part.onArrowPierced += func;
        }
    }

    public void ReleaseRagdoll()
    {
        foreach (BodyPart part in bodyParts)
        {
            if (part.GetBodyType() != BodyPartType.Chest)
                bodyPartBlood.BloodSplash(part.GetBodyType());

            part.Release();
        }

        foreach (FollowObject followObject in followObjects)
        {
            followObject.Disable();
        }
    }

    public void LooseJoint()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.Loose();
        }

        foreach (FollowObject followObject in followObjects)
        {
            followObject.Disable();
        }
    }

    public void BodyFade()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.Fade();
        }
    }

    public void BodyColorReset()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.SetDefaultColor();
        }
    }

    public void BodyGetPoison()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.SlowlyPoison();
        }
    }

    public void InjuredBody()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.Injured();
        }
    }

    public void BodyFreeze()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.Freeze();
        }
    }

    public void BodyBuzzing()
    {
        foreach (BodyPart part in bodyParts)
        {
            part.Buzzing();
        }
    }

    public List<Rigidbody> GetBodyPartsRigid()
    {
        List<Rigidbody> bodypartsRigid = new List<Rigidbody>();
        foreach (BodyPart part in bodyParts)
        {
            bodypartsRigid.Add(part.rb);
        }
        return bodypartsRigid;
    }

    public Rigidbody GetBodyBeDraged()
    {
        Rigidbody body = new Rigidbody();
        foreach (BodyPart part in bodyParts)
        {
            if (part.GetBodyType() == BodyPartType.Chest)
                body = part.rb;
        }
        return body;
    }
}
