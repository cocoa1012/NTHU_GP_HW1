using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {
	[Header("滑鼠")]
    float rY;
    float mouseSpeed;

	[Header("攝影機")]
    GameObject cam;
	float angle;
	void Start()
	{
		mouseSpeed = 5.0f;	
		cam = GameObject.Find("Main Camera");	
	}
	// Update is called once per frame
	void Update () {
		rY = Input.GetAxis("Mouse Y");
		angle = cam.transform.eulerAngles.x + (-rY * mouseSpeed);
		if (!(angle > 35 && angle < 325)){
            print(angle);
            cam.transform.RotateAround(transform.position, cam.transform.right, mouseSpeed * -rY);
        }
	}
}
