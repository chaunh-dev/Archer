using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHead : MonoBehaviour
{
    [SerializeField] protected Arrow arrow;
    [SerializeField] protected GameObject bloodEff;
    [SerializeField] protected Collider arrowHeadCollider;
    [SerializeField] Collider arrowCollider;
    [SerializeField] Collider extraIgnoreCol;

    protected virtual void Start()
    {
        GetComponent<BoxCollider>().enabled = true;

        Physics.IgnoreCollision(arrowHeadCollider, arrowCollider);
        if (extraIgnoreCol != null)
            Physics.IgnoreCollision(arrowHeadCollider, extraIgnoreCol);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter " + other.name);


        //joint.connectedBody = other.GetComponent<Rigidbody>();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collide" + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
            arrow.ShakeArrow();

        arrow.OnArrowHeadCollide(collision.collider);


        GetComponent<BoxCollider>().enabled = false;
        if (collision.collider.GetComponent<BodyPart>() == null)
        {
            arrow.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            bloodEff.SetActive(true);
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Arrow-1"))
        {
            collision.gameObject.GetComponent<BoxCollider>().enabled = false;
            arrow.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
