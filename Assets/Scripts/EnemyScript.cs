using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    bool calculated;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Bullet") && !calculated)
        {
            GameManager.instance.EnemyDiedIncrementer();
            calculated = true;
        }
    }

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (!calculated)
    //    {
    //        GameManager.instance.EnemyDiedIncrementer();
    //        calculated = true;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water") && !calculated)
        {
            GameManager.instance.EnemyDiedIncrementer();
            calculated = true;
        }
    }
}
