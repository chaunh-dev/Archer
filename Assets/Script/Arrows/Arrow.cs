using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.CodeDom;
using System.Runtime.InteropServices.WindowsRuntime;

public class Arrow : MonoBehaviour
{
    [SerializeField] GameObject arrowHead;
    public Rigidbody rb;
    [SerializeField] protected Rigidbody headRb;
    [SerializeField] float force;
    [SerializeField] protected List<Collider> colliders = new List<Collider>();
    [SerializeField] Material defaultMat;
    [SerializeField] Material holdArrowMat;
    [SerializeField] GameObject holdEffect;
    [SerializeField] GameObject blackSprite;

    protected float fireForce;
    [SerializeField] protected bool isFired;
    [SerializeField] protected bool isFlying;
    protected bool isOnHead;
    protected float timeSinceOnHeadCollision = 0;
    protected float timePassedSinceLaunch = 0;
    public bool IsOnHead { get => isOnHead; set => isOnHead = IsOnHead; }

    protected void Awake()
    {
        colliders.AddRange(GetComponentsInChildren<Collider>());
        // colliders.Add(GetComponent<Collider>());
    }

    public void OnHold(bool _isHold)
    {
        if (this == null || !gameObject.activeInHierarchy)
        {
            return;
        }

        if (holdEffect == null & blackSprite == null)
            return;

        if (_isHold)
        {
            GetComponent<SpriteRenderer>().material = holdArrowMat;
            holdEffect.SetActive(true);
            blackSprite.SetActive(true);
        }
        else
        {
            GetComponent<SpriteRenderer>().material = defaultMat;
            holdEffect.SetActive(false);
            blackSprite.SetActive(false);
        }
    }

    public virtual void Detach()
    {
        if (this == null || !gameObject.activeInHierarchy)
        {
            return;
        }

        transform.parent = null;
        Vector3 currentScale = transform.localScale;
        currentScale.y = Mathf.Abs(currentScale.y);
        transform.localScale = currentScale;
        rb.isKinematic = false;
        rb.useGravity = true;
        isFired = true;
        isFlying = true;
        timePassedSinceLaunch = 0;

        DOVirtual.DelayedCall(0.1f, () =>
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = false;
            }
        });

        DOVirtual.DelayedCall(0.3f, () =>
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        });
    }

    public void EndGameDetach()
    {
        transform.parent = null;
        Vector3 currentScale = transform.localScale;
        currentScale.y = Mathf.Abs(currentScale.y);
        transform.localScale = currentScale;
        rb.isKinematic = false;
        rb.useGravity = false;
        isFired = true;
        isFlying = true;
        timePassedSinceLaunch = 0;

        DOVirtual.DelayedCall(0.2f, () =>
        {
            foreach (Collider collider in colliders)
            {
                collider.enabled = true;
            }
        });
    }

    public void AddForce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.VelocityChange);
    }

    public void SaveForce(float force)
    {
        fireForce = force;
    }

    // Start is called before the first frame update
    protected virtual void OnEnable()
    {
        DisableColliders();

        rb.drag = GameConfigManager.instance.ragdoll.GetArrowDrag();
        headRb.drag = GameConfigManager.instance.ragdoll.GetArrowDrag();
    }

    protected void DisableColliders()
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isFired && isFlying)
        {
            transform.LookAt(transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0));
            transform.Rotate(0, -90, 0);
        }

        if (isFired && !isOnHead)
        {
            timePassedSinceLaunch += Time.deltaTime;
        }

        if (isOnHead)
        {
            timeSinceOnHeadCollision += Time.deltaTime;
        }

        if (timeSinceOnHeadCollision > 5)
        {
            Destroy(gameObject);
        }

        if (timePassedSinceLaunch > 8)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnArrowHeadCollide(Collider other)
    {
        if (!isFired)
        {
            return;
        }

        if (!isFlying)
        {
            return;
        }


        isFlying = false;
        isOnHead = true;

        float desiredForce = force;

        if (other.gameObject.tag == "Head")
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Archer-2"))
            {
                LevelController.Instance.OnHeadShot(other.transform);
                other.gameObject.GetComponentInParent<BodyPartBlood>().BloodSplash(BodyPartType.Head);
                ConfigurableJoint headJoint = other.GetComponent<ConfigurableJoint>();
                Destroy(headJoint);
            }
        }

        BodyPart bodyPart = other.GetComponent<BodyPart>();
        if (bodyPart)
        {
            bodyPart.onArrowPierced(bodyPart, this);
        }

        if (other.gameObject.layer != LayerMask.NameToLayer("Arrow-2"))
        {
            if (other.gameObject.tag != "Head")
                AttachArrowToTarget(other);
        }
    }

    protected virtual void AttachArrowToTarget(Collider other)
    {
        transform.parent = other.gameObject.transform;
        transform.position += transform.right * 0.5f;
        rb.velocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        rb.isKinematic = true;
        rb.useGravity = false;
        GetComponent<Collider>().enabled = false;
    }

    public void ShakeArrow()
    {
        float swingDuration = 0.15f;
        float swingAngle = 5f;

        Sequence swingSequence = DOTween.Sequence();

        swingSequence.Append(transform.DORotate(new Vector3(0, 0, swingAngle), swingDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad))
                     .Append(transform.DORotate(new Vector3(0, 0, -swingAngle * 2), swingDuration * 2, RotateMode.LocalAxisAdd).SetEase(Ease.InOutQuad))
                     .Append(transform.DORotate(new Vector3(0, 0, swingAngle), swingDuration, RotateMode.LocalAxisAdd).SetEase(Ease.OutQuad));
    }
}