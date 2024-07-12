using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowStringControl : MonoBehaviour
{
    [SerializeField] private Transform stringStart;
    [SerializeField] private Transform stringMid;
    [SerializeField] private Transform stringEnd;
    [SerializeField] private LineRenderer stringRenderer;
    [SerializeField] private float stringWidth;
    private Vector3 initialStringMidPosition;

    private void Start()
    {
        stringRenderer = GetComponent<LineRenderer>();
        if (stringRenderer == null)
        {
            stringRenderer = gameObject.AddComponent<LineRenderer>();
        }

        stringRenderer.positionCount = 3;
        stringRenderer.startWidth = stringWidth;
        stringRenderer.endWidth = stringWidth;
        initialStringMidPosition = stringMid.localPosition;
    }

    private void Update()
    {
        stringRenderer.SetPosition(0, stringStart.position);
        stringRenderer.SetPosition(1, stringMid.position);
        stringRenderer.SetPosition(2, stringEnd.position);
    }

    public void UpdateString(Vector3 pullPosition, bool isPulling, float pullPercent = 0)
    {
        if (isPulling)
        {
            Vector3 targetPosition = Vector3.Lerp(initialStringMidPosition, pullPosition, pullPercent);
            stringMid.localPosition = new Vector3(0, targetPosition.x, 0);
        }
        else
        {
            stringMid.localPosition = initialStringMidPosition;
        }
    }

    public Transform GetStringMid()
    {
        return stringMid;
    }

}
