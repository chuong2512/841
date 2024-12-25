using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObject : MonoBehaviour
{

    public Vector3 force;
    public GameObject blastEffect;
    Rigidbody rb;
    int blastCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.AddRelativeForce(force, ForceMode.Impulse);
        Destroy(this.gameObject, 4);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Destroy(this.gameObject);
    //}

    private void OnCollisionEnter(Collision other)
    {
        rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb)
        {
            if(blastCounter == 0)
            {
                Instantiate(blastEffect, this.transform.position, blastEffect.transform.rotation);
                blastCounter = 1;
            }
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddRelativeForce(new Vector3(0, 2, 10), ForceMode.Impulse);
        }
    }
}
