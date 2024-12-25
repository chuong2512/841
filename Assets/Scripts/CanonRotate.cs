using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CanonRotate : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 firstPos;
    Vector3 secondPos;
    public float speed;

    public Transform cross;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && GameManager.instance.startCannonRotation)
        {
            firstPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }

        if(Input.GetMouseButton(0) && GameManager.instance.startCannonRotation)
        {
            secondPos = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Vector3 difference = secondPos - firstPos;
            //cross.position = new Vector3(difference.x * speed, difference.y * speed, cross.position.z);
            Quaternion diff = Quaternion.Euler(-difference.y * speed, difference.x * speed,  difference.z * speed);
            transform.rotation = diff;
            cross.rotation = Quaternion.Euler((cross.rotation.x - this.transform.rotation.x), (cross.rotation.y - this.transform.rotation.y), 0);

            
            //firstPos = secondPos;
        }
    }
}
