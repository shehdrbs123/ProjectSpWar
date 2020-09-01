using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InGameButtonScript : NetworkBehaviour {
	[HideInInspector]
	private NetworkLobbyManagerExtend lobbyManager;
	public GameObject[] UI = new GameObject[3];
	enum UINAME{
		MAIN_UI,DIALOG_UI,PAUSE_UI
	};
	public delegate void exitdelegate();
	private exitdelegate exit;
	void Start(){
		lobbyManager = NetworkLobbyManagerExtend._singleton;
	}

	public void OnPressPauseButton(){
		UI [(int)UINAME.PAUSE_UI].SetActive (true);
	}

	public void OnPressPauseUIButtons(Button button){
		if(button.GetComponentInChildren<Text>().text.Equals("Exit")){
			if (!isServer) {
				lobbyManager.StopClient ();
			} else
				lobbyManager.StopHost ();
		}else{
			UI [(int)UINAME.PAUSE_UI].SetActive (false);
		}
	}
}
