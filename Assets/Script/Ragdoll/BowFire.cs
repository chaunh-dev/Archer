using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowFire : MonoBehaviour
{
    [SerializeField] Arrow arrow;
    [SerializeField] GameObject arrowHolder;
    [SerializeField] float minFireForce;
    [SerializeField] float maxFireForce;
    [SerializeField] float maxPullDuration;
    [SerializeField] FollowObject leftHand;
    [SerializeField] bool receiveUserInput;
    [SerializeField] BowStringControl bowString;
    [SerializeField] Transform aimStringLimit;
    [SerializeField] float miniShotDelay = 0.2f;
    [SerializeField] float miniShotTimer = 0f;
    [SerializeField] float miniShotCooldown = 2f;

    private bool isHoldingArrow;
    private Arrow holdingArrow;
    private float holdingDuration;
    private float timer = 0f;
    private bool isFiring = false;

    private void OnEnable()
    {
        minFireForce = GameConfigManager.instance.ragdoll.GetMinFireForce();
        maxFireForce = GameConfigManager.instance.ragdoll.GetMaxFireForce();
        maxPullDuration = GameConfigManager.instance.ragdoll.GetBowPullDuration();
    }

    // Update is called once per frame
    void Update()
    {
        if (receiveUserInput)
        {
            timer += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                holdingDuration = 0;
                PutArrowOnBow();
            }

            if (Input.GetMouseButton(0))
            {
                UpdateStringByPercent(holdingDuration, maxPullDuration);
                if (holdingDuration / maxPullDuration > 0.8f)
                {
                    if (holdingArrow != null)
                        holdingArrow.OnHold(true);
                }
            }


            if (Input.GetMouseButtonUp(0))
            {
                if (holdingArrow == null)
                    return;

                if (isHoldingArrow)
                {
                    isFiring = true;
                    if (bowString != null)
                        bowString.UpdateString(Vector3.zero, false);
                }
            }
        }

        if (isHoldingArrow)
        {
            holdingDuration += Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        if (isFiring)
        {
            FireArrow();
            isFiring = false;
        }
    }

    public void UpdateStringByPercent(float _min, float _max)
    {
        if (holdingArrow == null)
        {
            isHoldingArrow = false;
            return;
        }

        if (bowString != null)
        {
            float pullPercent = Mathf.Clamp01(_min / _max);
            bowString.UpdateString(aimStringLimit.localPosition, true, pullPercent);

            if (isHoldingArrow)
            {
                Vector3 tempPos = new Vector3(0.6f, 0f, 0f);
                holdingArrow.transform.localPosition = -tempPos * pullPercent;
                holdingArrow.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }

        }
    }

    public void PutArrowOnBow()
    {
        if (isHoldingArrow)
        {
            return;
        }

        holdingArrow = Instantiate(arrow, arrowHolder.transform).GetComponent<Arrow>();


        holdingArrow.gameObject.SetActive(true);
        leftHand.EnableFollow(true);
        isHoldingArrow = true;

    }

    public void FireArrow()
    {
        if (!isHoldingArrow)
        {
            return;
        }

        if (holdingArrow == null)
        {
            isHoldingArrow = false;
            return;
        }

        float fireForce = minFireForce + (maxFireForce - minFireForce) * Mathf.Clamp01(holdingDuration / maxPullDuration);
        holdingArrow.SaveForce(fireForce);
        holdingArrow.Detach();

        if (receiveUserInput)
            holdingArrow.OnHold(false);

        holdingArrow.AddForce(holdingArrow.transform.right * fireForce);
        leftHand.EnableFollow(false);
        isHoldingArrow = false;
    }

    public Transform GetArrowPos()
    {
        if (holdingArrow != null)
        {
            return holdingArrow.transform;
        }

        return null;
    }

    public float GetHoldTimePercent()
    {
        return Mathf.Clamp01(holdingDuration / maxPullDuration);
    }

    public float GetFireForce()
    {
        float fireForce = minFireForce + (maxFireForce - minFireForce) * Mathf.Clamp01(holdingDuration / maxPullDuration);
        return fireForce;
    }

    public void FireArrowEndGame()
    {
        if (!isHoldingArrow)
        {
            return;
        }

        holdingArrow.EndGameDetach();

        holdingArrow.AddForce(holdingArrow.transform.right * maxFireForce);
        leftHand.EnableFollow(false);
        isHoldingArrow = false;
        if (bowString != null)
            bowString.UpdateString(Vector3.zero, false);
    }

    public bool IsHoldingArrow()
    {
        return isHoldingArrow;
    }
}
