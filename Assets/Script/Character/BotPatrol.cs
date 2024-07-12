using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotPatrol : MonoBehaviour
{
    public PatrolType patrolType;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float speed = 3f;
    [SerializeField] private bool isPatrolBot = false;
    private bool movingPositive = true;
    private Vector3 startPosition;
    public enum PatrolType
    {
        Vertical,
        Horizontal,
    }

    private void Awake()
    {
        isPatrolBot = false;
        startPosition = this.transform.position;
    }

    private void Update()
    {
        if (isPatrolBot)
            Moving();
    }

    private void Moving()
    {
        float currentPosition = (patrolType == PatrolType.Horizontal) ? transform.position.x : transform.position.y;
        float startPositionAxis = (patrolType == PatrolType.Horizontal) ? startPosition.x : startPosition.y;
        Vector3 movementVector = (patrolType == PatrolType.Horizontal) ? new Vector3(speed * Time.deltaTime, 0, 0) : new Vector3(0, speed * Time.deltaTime, 0);

        float positiveLimit = startPositionAxis + patrolDistance;
        float negativeLimit = startPositionAxis - patrolDistance;

        if (movingPositive)
        {
            transform.position += movementVector;

            if (currentPosition >= positiveLimit)
            {
                movingPositive = false;
            }
        }
        else
        {
            transform.position -= movementVector;

            if (currentPosition <= negativeLimit)
            {
                movingPositive = true;
            }
        }
    }

    public void IsPatrolBot(bool _isPatrolBot)
    {
        isPatrolBot = _isPatrolBot;
    }

    public void SetPatrolType(PatrolType _patrolType)
    {
        patrolType = _patrolType;
    }
}
