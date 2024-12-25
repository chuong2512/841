using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diano : MonoBehaviour
{
    Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
