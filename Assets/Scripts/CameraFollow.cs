using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followObject;

    int screenWidth;
    int screenHeight;

    private void Awake()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        if(screenWidth == 720 && screenHeight == 1280)
        {
            Camera.main.fieldOfView = 68;
        }

        if(screenWidth == 1080 && screenHeight == 2340)
        {
            Camera.main.fieldOfView = 78;
        }
    }
    private void LateUpdate()
    {
        if(GameManager.instance.cameraFollow)
        {
            this.transform.position = new Vector3(0, 14, followObject.position.z - 17);
        }
    }
}
