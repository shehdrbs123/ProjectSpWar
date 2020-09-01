using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class Skill : NetworkBehaviour {
	public int coolTime;
	public GameObject skillObject;
	public override void OnStartServer ()
	{
		base.OnStartServer ();
		GameObject skillob = Instantiate (skillObject,transform.position,transform.rotation);
		NetworkServer.Spawn (skillob);
		Destroy (skillob, 5f);
	}
}
