using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ConnectSceneButtonScript : MonoBehaviour {
	[HideInInspector]
	public NetworkLobbyManagerExtend lobbyManager;
	public RectTransform[] UI = new RectTransform[5];
	[HideInInspector]
	public RectTransform presentUI;

    private const int UI_MAIN = 0;
    private const int UI_HOST = 1;
    private const int UI_CLI = 2;
    private const int UI_WAIT = 3;
    private const int UI_POP = 4;
	void Start(){
		lobbyManager = (NetworkLobbyManagerExtend)NetworkLobbyManagerExtend.singleton;
		lobbyManager.ui = this;
		presentUI = UI [UI_MAIN];
	}
	private delegate void exitButtonDelegate();
	private exitButtonDelegate exitDelegate;

	public void UIChangeTo(char name){
		if (presentUI != null) {
			presentUI.gameObject.SetActive (false);
		}
        int num = 0;
        switch (name)
        {
            case 'm' : num = 0;
                break;
            case 'h':  num = 1;
                break;
            case 'c':  num = 2;
                break;
            case 'w':  num = 3;
                break;
            case 'p':  num = 4;
                break;
        }
        presentUI = UI[num];
		if (presentUI != null) {
			presentUI.gameObject.SetActive (true);
		}
	}

	//button method

	public void OnClickConnectionInHCSelect(){
		UIChangeTo ('c');
	}
	public void OnClickHostInHCSelect(){
        UIChangeTo('h');
	}

	public void OnClickCreateInHost(){
		InputField[] inputField = UI [UI_HOST].GetComponentsInChildren<InputField> ();
		try{
			lobbyManager.localplayerName= inputField [0].GetComponentsInChildren<Text> ()[1].text;
			lobbyManager.networkPort = int.Parse (inputField [1].GetComponentsInChildren<Text> ()[1].text);
			lobbyManager.StartHost();
			if(UnityEngine.Networking.NetworkServer.active){
				UIChangeTo ('w');
				setupWaitroom();
			}
		}catch(System.Exception e){
			Debug.Log (e.GetBaseException ());
		}
	
	}
	public void OnClickClientConnect(){
		InputField[] inputField = UI [UI_CLI].GetComponentsInChildren<InputField> ();
		try{
			lobbyManager.localplayerName = inputField [0].GetComponentsInChildren<Text>()[1].text;
			lobbyManager.networkAddress = inputField [1].GetComponentsInChildren<Text> ()[1].text;
			lobbyManager.networkPort = int.Parse (inputField [2].GetComponentsInChildren<Text> ()[1].text);
			lobbyManager.StartClient();
			if(!UnityEngine.Networking.NetworkServer.active){
				UIChangeTo ('w');
				setupWaitroom();
			}
		}catch(System.Exception e){
			Debug.Log (e.StackTrace);
		}
	}
	public void setupWaitroom(){
		Text[] serverInfo = UI [UI_WAIT].Find("RoomInfo").GetComponentsInChildren<Text>();
		serverInfo[0].text = "Server : " + lobbyManager.networkAddress + ":" + lobbyManager.networkPort;
		if (NetworkServer.active) 
			exitDelegate = lobbyManager.StopHost;
		else
			exitDelegate = lobbyManager.StopClient;
	}
	public void OnClickExitInConnectScene(){
		if (presentUI == UI [UI_WAIT]) {
			if (lobbyManager.isNetworkActive) {
				exitDelegate ();
			}
		}
        UIChangeTo('m');
	}

}
