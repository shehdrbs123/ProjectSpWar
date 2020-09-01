using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UserUIButton: ParentButton{
	//[HideInInspector]
	public Transform target;//GamePlayer에서 초기화
	//[HideInInspector]
	public bool isPressed;
	private RectTransform HealthBar;
	private float maxHealth;
	public void init(Transform target){
		this.target = target;
		HealthBar = transform.GetChild(0).GetComponent<RectTransform>();
		HealthBar.sizeDelta = new Vector2 (160, HealthBar.rect.height);
		maxHealth = HealthBar.rect.width;
		transform.GetChild (1).GetComponent<Text> ().text = target.gameObject.GetComponent<GamePlayer> ().nickname;
	}
	public override void OnPointerDown (PointerEventData pEvent){
		if (player != null && target != null) {
			UserUIButton who = UserUIButtonMon.isPressed ();
			if (who == null) {//None Pressed
				player.DimensionSupportTarget = this;
				isPressed = true;
				GetComponent<Image> ().color = Color.gray;
			} else if(who == this) {//this Pressed 
				player.DimensionSupportTarget = null;
				isPressed = false;
				GetComponent<Image> ().color = Color.white;
			}//otherPressed
		}
	}
	public void AfterResetButton(){
		player.DimensionSupportTarget = null;
		isPressed = false;
		GetComponent<Image> ().color = Color.white;
	}

	public void SyncHealthBar(int Health){
		HealthBar.sizeDelta = new Vector2 (maxHealth * Health / 1000, HealthBar.rect.height);
	}

}
