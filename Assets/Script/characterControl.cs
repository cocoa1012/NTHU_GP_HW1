using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class characterControl : MonoBehaviour {

	// Animator m_anim;
    Animator m_anim;
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
    // float rY;
    float mouseSpeed;

    // [Header("攝影機")]
    // GameObject cam;
    // GameObject pivot;
    // float angle;
    [Header("是否著地")]
    private bool landing;
    [Header("UI")]
    public Text coinText;
    private int numOfCoins;
    public Image shield;
    private bool isProtected;

    public Canvas gameOver;

    // Use this for initialization
    void Start () {
        m_anim = this.gameObject.GetComponent<Animator>();
        m_rigid = this.gameObject.GetComponent<Rigidbody>();
        initPos = this.gameObject.transform.position;
        // cam = GameObject.Find("Main Camera");
        // pivot = GameObject.Find("_root");
        // print(pivot);

        speed = 10.0f;
        mouseSpeed = 5.0f;
        landing = true;
        numOfCoins = 10;
        shield.enabled = false;
        isProtected = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Anim_Machine();
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
            if (landing == true){
                landing = false;
                m_anim.SetBool("isJump", true);
                m_rigid.AddForce(new Vector3(0, 700, 0));
            }
        }

        rX = Input.GetAxis("Mouse X");
        // rY = Input.GetAxis("Mouse Y");
		transform.Rotate(0.0f, mouseSpeed * rX, 0.0f);
        // angle = cam.transform.eulerAngles.x + (-rY * mouseSpeed);
        // if (!(angle > 35 && angle < 325)){
        //     print(angle);
        //     cam.transform.RotateAround(pivot.transform.position, cam.transform.right, mouseSpeed * -rY);
        // }
	}

	private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "coin"){
            print ("get coin!");
            Destroy(collision.gameObject);
            numOfCoins -= 1;
            coinText.text = "Coins Remaining: " + numOfCoins;
        }
        if (collision.gameObject.tag == "shield"){
            shield.enabled = true;
            isProtected = true;
            Canvas Info = GameObject.Find("shieldInfo").GetComponent<Canvas>();
            Info.enabled = true;
            Cursor.visible = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "key1"){
            print ("open door1!");
            GameObject door = GameObject.Find("door1");
            door.GetComponent<Transform>().Translate(0, 10, 0);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "key2"){
            print ("open door2!");
            GameObject door = GameObject.Find("door2");
            door.GetComponent<Transform>().Translate(0, 10, 0);
            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "floor"){
            landing = true;
            m_anim.SetBool("isJump", false);
        }
        if (other.gameObject.tag == "spring"){
            landing = false;
            m_anim.SetBool("isJump", true);
            m_rigid.AddForce(new Vector3(0, 1800, 0));
        }
        if (other.gameObject.tag == "ball"){
            if (isProtected){
                shield.enabled = false;
                isProtected = false;
            }
            else{
                gameOver.enabled = true;
                Cursor.visible = true;
                Time.timeScale = 0;
            }
        }
    }

    //Don't touch
    void Anim_Machine()
    {
        if (Mathf.Abs(transform.position.x - curPos.x) > 0.1f || Mathf.Abs(transform.position.z - curPos.z) > 0.1f)
            m_anim.SetBool("isRun", true);
        else
            m_anim.SetBool("isRun", false);
            
            
    }
}
