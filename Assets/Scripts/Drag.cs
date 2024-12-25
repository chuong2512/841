using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    Vector3 firstValue;
    Vector3 secondValue;
    public float speed = 15;

    float minX = -7;
    float maxX = 7;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GameManager.instance.dragControl)
        {
            firstValue = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
        }

        if (Input.GetMouseButton(0) && GameManager.instance.dragControl)
        {
            secondValue = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, 0, 0));
            Vector3 diff = secondValue - firstValue;
            transform.position += diff * speed;
            this.transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), this.transform.position.y, this.transform.position.z);
            firstValue = secondValue;
        }
    }
}
