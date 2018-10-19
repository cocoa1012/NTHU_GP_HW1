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

    
    [Header("是否著地")]
    private bool landing;
    [Header("UI")]
    public Text coinText;
    private int numOfCoins;
    public Image shield;
    private bool isProtected;

    public Canvas gameOver;
    public Canvas ESC;
    public Canvas FINISH;

    public AudioClip GATE;
    public AudioClip COIN;
    public AudioClip SHIELD;
    public AudioClip JUMP;
    
    AudioSource BGM;

    // Use this for initialization
    void Start () {
        m_anim = this.gameObject.GetComponent<Animator>();
        m_rigid = this.gameObject.GetComponent<Rigidbody>();
        initPos = this.gameObject.transform.position;
        BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
        

        speed = 10.0f;
        mouseSpeed = 5.0f;
        landing = true;
        numOfCoins = 10;
        shield.enabled = false;
        isProtected = false;
        FINISH.enabled = false;
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
                BGM.PlayOneShot(JUMP);
                landing = false;
                m_anim.SetBool("isJump", true);
                m_rigid.AddForce(new Vector3(0, 700, 0));
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)){
            Cursor.visible = true;
            ESC.enabled = true;
            Time.timeScale = 0;
        }

        rX = Input.GetAxis("Mouse X");
        
		transform.Rotate(0.0f, mouseSpeed * rX, 0.0f);
        
	}

	private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "coin"){
            BGM.PlayOneShot(COIN);
            print ("get coin!");
            Destroy(collision.gameObject);
            numOfCoins -= 1;
            coinText.text = "Coins Remaining: " + numOfCoins;
        }
        if (collision.gameObject.tag == "shield"){
            BGM.PlayOneShot(SHIELD);
            shield.enabled = true;
            isProtected = true;
            Canvas Info = GameObject.Find("shieldInfo").GetComponent<Canvas>();
            Info.enabled = true;
            Cursor.visible = true;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "key1"){
            BGM.PlayOneShot(GATE);
            print ("open door1!");
            GameObject door = GameObject.Find("door1");
            door.GetComponent<Transform>().Translate(0, 10, 0);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "key2"){
            BGM.PlayOneShot(GATE);
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
            BGM.PlayOneShot(JUMP);
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
        if (other.gameObject.tag == "exitWall"){
            if (numOfCoins == 0){
                GameObject.Destroy(other.gameObject);
            }
        }
        if (other.gameObject.tag == "exit"){
            if (numOfCoins == 0){
                Cursor.visible = true;
                FINISH.enabled = true;
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
