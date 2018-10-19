using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour {

	Canvas title;
	Canvas Info;
	Canvas shieldInfo;
	Canvas gameOver;
	Canvas ESC;
	
	// Use this for initialization
	AudioSource BGM;
	public AudioClip BTN;

	Light L;
	void Start () {
		title = GameObject.Find("Title").GetComponent<Canvas>();
		Info = GameObject.Find("Info").GetComponent<Canvas>();
		shieldInfo = GameObject.Find("shieldInfo").GetComponent<Canvas>();
		gameOver = GameObject.Find("gameOver").GetComponent<Canvas>();
		ESC = GameObject.Find("ESC").GetComponent<Canvas>();
		L = GameObject.Find("Directional Light").GetComponent<Light>();
		BGM = GameObject.Find("BGM").GetComponent<AudioSource>();
		title.enabled = true;
		Info.enabled = false;
		shieldInfo.enabled = false;
		gameOver.enabled = false;
		ESC.enabled = false;
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quitApp(){
		BGM.PlayOneShot(BTN);
		Application.Quit();
	}

	public void hideTitle(){
		BGM.PlayOneShot(BTN);
		title.enabled = false;
		Cursor.visible = false;
		Time.timeScale = 1;
	}

	public void showInfo(){
		BGM.PlayOneShot(BTN);
		Info.enabled = true;
	}

	public void hideInfo(){
		BGM.PlayOneShot(BTN);
		Info.enabled = false;
	}
	public void hideshieldInfo(){
		BGM.PlayOneShot(BTN);
		shieldInfo.enabled = false;
		Cursor.visible = false;
	}

	public void hideESC(){
		BGM.PlayOneShot(BTN);
		Cursor.visible = false;
		ESC.enabled = false;
		Time.timeScale = 1;
	}

	public void volume(float v){
		BGM.volume = v;
	}
	public void bright(float b){
		L.intensity = b;
	}

	public void retry(){
		BGM.PlayOneShot(BTN);
		gameOver.enabled = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
