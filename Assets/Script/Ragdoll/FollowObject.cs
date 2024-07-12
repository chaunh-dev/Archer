using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject objectToFollow;
    public float force;
    public Rigidbody rb;
    public Rigidbody connectBodyRb;
    public ForceMode forceMode;

    [SerializeField] bool enableFollow;
    [SerializeField] bool usingAccelerationForce;
    [SerializeField] float forceAcceleration;

    private float currentForce;

    public void EnableFollow(bool enable)
    {
        rb.velocity = Vector3.zero;
        connectBodyRb.velocity = Vector3.zero;
        enableFollow = enable;
    }

    public void Disable()
    {
        EnableFollow(false);

        rb.mass = 1;
        rb.drag = 0;
        rb.angularDrag = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!enableFollow)
        {
            currentForce = 0;
            return;
        }

        Vector3 forceDirection = objectToFollow.transform.position - transform.position;

        float forceToBeApplied = force;

        if (usingAccelerationForce)
        {
            currentForce = forceAcceleration;
            // currentForce += forceAcceleration * Time.fixedDeltaTime;
            forceToBeApplied = currentForce;
        }

        rb.AddForce(forceDirection * forceToBeApplied, forceMode);
    }
}
