using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartBlood : MonoBehaviour
{
    [SerializeField] GameObject[] head;
    [SerializeField] GameObject[] leftArm1;
    [SerializeField] GameObject[] leftArm2;
    [SerializeField] GameObject[] rightArm1;
    [SerializeField] GameObject[] rightArm2;
    [SerializeField] GameObject[] leftLeg1;
    [SerializeField] GameObject[] leftLeg2;
    [SerializeField] GameObject[] rightLeg1;
    [SerializeField] GameObject[] rightLeg2;

    public void BloodSplash(BodyPartType _bodyPartType)
    {
        switch (_bodyPartType)
        {
            case BodyPartType.Head:
                {
                    foreach (GameObject eff in head)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    break;
                }

            case BodyPartType.Arm:
                {
                    foreach (GameObject eff in leftArm1)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    foreach (GameObject eff in leftArm2)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    foreach (GameObject eff in rightArm1)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    foreach (GameObject eff in rightArm2)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    break;
                }

            case BodyPartType.Leg:
                {
                    foreach (GameObject eff in leftLeg1)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    foreach (GameObject eff in leftLeg2)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    foreach (GameObject eff in rightLeg1)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    foreach (GameObject eff in rightLeg2)
                    {
                        eff.gameObject.SetActive(true);
                    }
                    break;
                }
        }
    }

}
