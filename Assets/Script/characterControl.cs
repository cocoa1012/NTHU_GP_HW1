using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterControl : MonoBehaviour {

	// Animator m_anim;
    Rigidbody m_rigid;
    // float speed;

    [Header("角色的起始位置")]
    public Vector3 initPos;

    [Header("角色目前的位置")]
    public Vector3 curPos;

	[Header("角色速度")]
    public float speed;

    [Header("滑鼠")]
    float rX;
    float rY;
    float mouseSpeed;

    [Header("攝影機")]
    GameObject cam;
    float angle;

    // Use this for initialization
    void Start () {
        // m_anim = this.gameObject.GetComponent<Animator>();
        m_rigid = this.gameObject.GetComponent<Rigidbody>();
        initPos = this.gameObject.transform.position;
        cam = GameObject.Find("Main Camera");

        speed = 10.0f;
        mouseSpeed = 5.0f;

    }
	
	// Update is called once per frame
	void Update ()
    {
        // Anim_Machine();
        curPos = this.gameObject.transform.position;
        
        if (Input.GetKey(KeyCode.W)){
            transform.Translate(0, 0, speed * Time.deltaTime);    
        }
        if (Input.GetKey(KeyCode.S)){
            transform.Translate(0, 0, -speed * Time.deltaTime);    
        }
        if (Input.GetKey(KeyCode.A)){
            transform.Translate(-speed * Time.deltaTime, 0, 0);    
        }
        if (Input.GetKey(KeyCode.D)){
            transform.Translate(speed * Time.deltaTime, 0, 0);    
        }
        if (Input.GetKeyDown(KeyCode.Space)){
            m_rigid.AddForce(new Vector3(0, 700, 0));
        }

        rX = Input.GetAxis("Mouse X");
        rY = Input.GetAxis("Mouse Y");
		transform.Rotate(0.0f, mouseSpeed * rX, 0.0f);
        angle = cam.transform.eulerAngles.x + (-rY * mouseSpeed);
        if (!(angle > 35 && angle < 325))
            cam.transform.RotateAround(transform.position, cam.transform.right, mouseSpeed * -rY);
	}

	private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "coin"){
            print ("get coin!");
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "key1"){
            print ("open!");
            GameObject door = GameObject.Find("Door1");
            door.GetComponent<Transform>().Translate(0, 10, 0);
            Destroy(collision.gameObject);
        }
    }

    //Don't touch
    // void Anim_Machine()
    // {
    //     if (Mathf.Abs(transform.position.x - curPos.x) > 0.1f || Mathf.Abs(transform.position.z - curPos.z) > 0.1f)
    //         m_anim.SetBool("isRunning", true);
    //     else
    //         m_anim.SetBool("isRunning", false);
    // }
}
