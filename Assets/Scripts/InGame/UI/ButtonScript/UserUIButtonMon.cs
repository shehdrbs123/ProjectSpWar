using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserUIButtonMon : MonoBehaviour {

	private static UserUIButton[] button;

	void Start(){
		button = new UserUIButton[3];
		button [0] = transform.GetChild (0).GetComponent<UserUIButton> ();
		button [1] = transform.GetChild (0).GetComponent<UserUIButton> ();
		button [2] = transform.GetChild (0).GetComponent<UserUIButton> ();
	}
	public static UserUIButton isPressed(){
		foreach (UserUIButton i in button) {
			if(i.isPressed){
				return i;
			}
		}
		return null;
	}
}
