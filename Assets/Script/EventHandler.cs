using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventHandler : MonoBehaviour {

	Canvas title;
	Canvas Info;
	Canvas shieldInfo;
	Canvas gameOver;
	// Use this for initialization
	void Start () {
		title = GameObject.Find("Title").GetComponent<Canvas>();
		Info = GameObject.Find("Info").GetComponent<Canvas>();
		shieldInfo = GameObject.Find("shieldInfo").GetComponent<Canvas>();
		gameOver = GameObject.Find("gameOver").GetComponent<Canvas>();
		title.enabled = true;
		Info.enabled = false;
		shieldInfo.enabled = false;
		gameOver.enabled = false;
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void quitApp(){
		Application.Quit();
	}

	public void hideTitle(){
		title.enabled = false;
		Cursor.visible = false;
		Time.timeScale = 1;
	}

	public void showInfo(){
		Info.enabled = true;
	}

	public void hideInfo(){
		Info.enabled = false;
	}
	public void hideshieldInfo(){
		shieldInfo.enabled = false;
		Cursor.visible = false;
	}

	public void retry(){
		gameOver.enabled = false;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
