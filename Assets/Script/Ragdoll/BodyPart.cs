using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BodyPart : MonoBehaviour
{
    [SerializeField] BodyPartType type;
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] ConfigurableJoint joint;
    [SerializeField] private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    private bool isJointDestroy = false;
    public Rigidbody rb;

    public Action<BodyPart, Arrow> onArrowPierced;

    public bool IsJointDestroy { get => isJointDestroy; }

    private void Start()
    {
        if (type == BodyPartType.Chest)
            sprites.AddRange(GetComponentsInChildren<SpriteRenderer>());
        else
            _renderer = GetComponent<SpriteRenderer>();
        joint = GetComponent<ConfigurableJoint>();
        rb = GetComponent<Rigidbody>();
    }

    public BodyPartType GetBodyType()
    {
        return type;
    }

    public void Injured()
    {
        if (type == BodyPartType.Chest)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                if (spr != null && !IsChildOfCharacterHealthBar(spr))
                {
                    spr.color = Color.red;
                }
            }
        }
        else
            _renderer.color = Color.red;
    }

    private bool IsChildOfCharacterHealthBar(SpriteRenderer spriteRenderer)
    {
        Transform parent = spriteRenderer.transform;
        while (parent != null)
        {
            if (parent.GetComponent<CharacterHealthBar>() != null)
            {
                return true;
            }
            parent = parent.parent;
        }
        return false;
    }

    public void Fade()
    {
        Color poisonColor = new Color(180f, 0f, 255f);
        if (type == BodyPartType.Chest)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                if (spr != null && !IsChildOfCharacterHealthBar(spr))
                {
                    // spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, 30f / 255f);
                    spr.color = poisonColor;

                }
            }
        }
        else
            // _renderer.color = new Color(_renderer.color.r, _renderer.color.g, _renderer.color.b, 30f / 255f);
            _renderer.color = poisonColor;

    }

    public void Poison()
    {
        Color poisonColor = new Color(180f, 0f, 255f);
        if (type == BodyPartType.Chest)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                if (spr != null && !IsChildOfCharacterHealthBar(spr))
                {
                    spr.color = poisonColor;
                }
            }
        }
        else
            _renderer.color = poisonColor;
    }

    public void SlowlyPoison()
    {
        Color poisonColor = new Color(180f, 0f, 255f);
        if (type == BodyPartType.Chest)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                if (spr != null && !IsChildOfCharacterHealthBar(spr))
                {
                    spr.color = Color.Lerp(spr.color, poisonColor, 0.3f);
                }
            }
        }
        else
            _renderer.color = Color.Lerp(_renderer.color, poisonColor, 0.3f);
    }

    public void Freeze()
    {
        Color freezeColor = new Color(129f / 255f, 240f / 255f, 255f / 255f);
        if (type == BodyPartType.Chest)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                if (spr != null && !IsChildOfCharacterHealthBar(spr))
                {
                    spr.color = freezeColor;
                }
            }
        }
        else
            _renderer.color = freezeColor;
    }

    public void SetDefaultColor()
    {
        if (type == BodyPartType.Chest)
        {
            foreach (SpriteRenderer spr in sprites)
            {
                if (spr != null && !IsChildOfCharacterHealthBar(spr))
                {
                    spr.color = Color.white;
                }
            }
        }
        else
            _renderer.color = Color.white;
    }

    public void Release()
    {
        Destroy(joint);
        isJointDestroy = true;

        // rb.constraints = RigidbodyConstraints.None;
        float randomX = Random.Range(-10, 10);
        float randomY = Random.Range(-10, 10);

        rb.AddForce(new Vector3(randomX, randomY, 0));
        rb.drag = 0;
        rb.angularDrag = 0;
        rb.angularVelocity = Vector3.zero;
    }

    public void Loose()
    {
        JointDrive jointDrive = joint.angularYZDrive;
        jointDrive.positionSpring = 100;
        joint.angularYZDrive = jointDrive;
    }

    public void Buzzing()
    {
        StartCoroutine(IEBuzzingEffect());
    }

    IEnumerator IEBuzzingEffect()
    {
        float duration = 2.0f; // duration of the effect
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            transform.localPosition += new Vector3(Random.Range(-0.08f, 0.08f), Random.Range(-0.08f, 0.08f), Random.Range(-0.08f, 0.08f));
            yield return new WaitForSeconds(0.05f);
            elapsed += 0.05f;
        }

    }
}
