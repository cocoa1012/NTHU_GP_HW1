using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		float rY = Input.GetAxis("Mouse Y");
		transform.Rotate(-5.0f * rY, 0.0f, 0.0f);
	}
}
