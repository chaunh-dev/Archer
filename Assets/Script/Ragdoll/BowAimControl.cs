using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BowAimControl : MonoBehaviour
{
    [SerializeField] GameObject aimRotateRoot;
    [SerializeField] GameObject dumpRotateRoot;
    [SerializeField] GameObject aimPos;
    [SerializeField] Camera _camera;
    [SerializeField] bool receiveUserInput;
    [Header("Trajectory")]
    [SerializeField] BowFire bowFire;
    [SerializeField] GameObject point;
    [SerializeField] float spaceBetweenPoint;
    [SerializeField] int numberOfPoint;
    [SerializeField] float trajectoryLineLimit;
    [SerializeField] GameObject[] trajectoryLine;

    private Vector3 startPos;
    private Vector3 velocity;
    private float acceleration;
    [SerializeField] private Vector3 currentLookPosition;

    void FixedUpdate()
    {
        if (receiveUserInput)
        {
            if (Input.GetButtonDown("Fire1") && !EventSystem.current.IsPointerOverGameObject())
            {
                acceleration = GetBaseVelocity();
                startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
                startPos.z = 0;
            }

            if (Input.GetButton("Fire1"))
            {
                // if (!EventSystem.current.IsPointerOverGameObject())
                // {
                Vector2 touchPosOnScreen = Input.mousePosition;
                Vector3 touchPosition = _camera.ScreenToWorldPoint(touchPosOnScreen);
                touchPosition.z = 0;
                Vector3 dragDirection = startPos - touchPosition;

                if (dragDirection.magnitude > trajectoryLineLimit)
                {
                    dragDirection = dragDirection.normalized;
                }

                touchPosition.z = 0;
                AimToPosition(dragDirection);
                if (bowFire.GetArrowPos() != null)
                {
                    velocity = bowFire.GetArrowPos().right * bowFire.GetFireForce();
                    if (dragDirection.x > 0)
                        DrawTrajectoryLine(aimPos.transform.position);
                }

                // SetTrajectoryLineActive(dragDirection.x > 0);
                // }
            }

            if (Input.GetButtonUp("Fire1"))
                SetTrajectoryLineActive(false);
        }
        else
        {
            SetTrajectoryLineActive(false);
        }

        ProcessMovingHandToAimedPosition();
    }

    void DrawTrajectoryLine(Vector3 arrowPos)
    {
        Vector3 currentPosition = arrowPos;

        if (trajectoryLine.Length == 0)
        {
            trajectoryLine = new GameObject[numberOfPoint];
            for (int i = 0; i < numberOfPoint; i++)
            {
                trajectoryLine[i] = Instantiate(point, transform.position, Quaternion.identity, aimPos.transform);
            }
        }

        int pointsToDisplay = Mathf.CeilToInt(numberOfPoint * bowFire.GetHoldTimePercent());


        for (int i = 0; i < numberOfPoint; i++)
        {
            if (i < pointsToDisplay)
            {
                float temp = i * spaceBetweenPoint;
                Vector3 pos = currentPosition + velocity * temp + 0.5f * Physics.gravity * temp * temp;
                trajectoryLine[i].transform.position = pos;
                if (i > 2)
                    trajectoryLine[i].SetActive(true);
            }
            else
            {
                trajectoryLine[i].SetActive(false);
            }
        }
    }

    private void SetTrajectoryLineActive(bool isActive)
    {
        if (trajectoryLine == null) return;

        foreach (GameObject point in trajectoryLine)
        {
            if (point != null)
            {
                point.SetActive(isActive);
            }
        }
    }

    public void AimToPosition(Vector3 position)
    {
        if (receiveUserInput)
        {
            currentLookPosition = position;
        }
        else
        {

            currentLookPosition = aimRotateRoot.transform.position - position;
            AdjustCurrentLookPosition();
        }
    }

    private void AdjustCurrentLookPosition()
    {
        if (currentLookPosition.y > -0.1f && currentLookPosition.y < 0.1f)
        {
            currentLookPosition.y = 0.1f;
        }
    }

    public void SetBaseVelocity()
    {
        acceleration = GetBaseVelocity();
    }

    public void SlowBowControl(int _multiTime, float _percent)
    {
        float reductionAmount = _multiTime * _percent * GetBaseVelocity();
        acceleration -= reductionAmount;
        acceleration = Mathf.Max(acceleration, 0);
    }

    private void ProcessMovingHandToAimedPosition()
    {
        if (receiveUserInput)
        {
            if (currentLookPosition.x > 0)
                dumpRotateRoot.transform.right = currentLookPosition;
        }
        else
        {
            dumpRotateRoot.transform.right = -currentLookPosition;
            dumpRotateRoot.transform.Rotate(180, 0, 0);
        }

        float desiredZ = dumpRotateRoot.transform.localEulerAngles.z;
        float currentZ = aimRotateRoot.transform.localEulerAngles.z;

        if (desiredZ > 180)
        {
            desiredZ -= 360;
        }

        if (currentZ > 180)
        {
            currentZ -= 360;
        }

        acceleration += (desiredZ - currentZ) * GetAcceleration();

        float cappedDiff = Mathf.Clamp(desiredZ - currentZ, -60, 60);
        float baseVelocity = cappedDiff / 60 * GetBaseVelocity();

        Vector3 desiredRotation = aimRotateRoot.transform.localEulerAngles;
        desiredRotation.z = currentZ + (baseVelocity + acceleration) * Time.fixedDeltaTime;

        aimRotateRoot.transform.localEulerAngles = desiredRotation;

        float dampeningForce = acceleration * GetDampening() * Time.fixedDeltaTime;
        acceleration -= dampeningForce;
    }

    public void SetCamera(Camera newCamera)
    {
        _camera = newCamera;
    }

    public void SetNumberOfPoint(int _numberOfPoint)
    {
        numberOfPoint = _numberOfPoint;
    }

    private float GetBaseVelocity()
    {
        return GameConfigManager.instance.ragdoll.GetBowControlBaseVelocity();
    }

    private float GetAcceleration()
    {
        return GameConfigManager.instance.ragdoll.GetBowControlAcceleration();
    }

    private float GetDampening()
    {
        return GameConfigManager.instance.ragdoll.GetBowControlDampening();
    }

    public bool IsReceivedControl()
    {
        return receiveUserInput;
    }
}
