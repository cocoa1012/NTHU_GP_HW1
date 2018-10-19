using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour {

	public Object ball;
	// Use this for initialization
	public float delayTime;
	private float alpha;
	public float speed;

	public AudioSource SE;
	public AudioClip CANON;
	void Start () {
		InvokeRepeating("shootBall", 2.0f, delayTime);
		alpha = 0.0f;
	}
	
	void shootBall(){
		SE.PlayOneShot(CANON);
		GameObject Ball = GameObject.Instantiate(ball, Vector3.zero, Quaternion.identity) as GameObject;
		Ball.transform.position = gameObject.transform.position + gameObject.transform.up * 0.5f;
		Ball.GetComponent<Rigidbody>().AddForce(gameObject.transform.up * 4000);
	}

	// Update is called once per frame
	void Update () {
		// transform.position.y = Mathf.PingPong(alpha,10);
		// alpha += speed;
	}
}
