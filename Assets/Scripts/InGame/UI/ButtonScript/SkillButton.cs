using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
public class SkillButton : ParentButton {
	public RectTransform cooldownBar;
	public int cooltime;
	public int ID;
	float maxHeight=200;
	public override void OnPointerDown (PointerEventData pEvent){
		if (enabled) {
			player.skillbuttonPressed = this;
		}
	}

	public IEnumerator coolTimeCoroutine(){
		enabled = false;
		cooldownBar.sizeDelta = new Vector2 (cooldownBar.rect.width, 200);
		for (int i = cooltime*10; i > 0; i--) {
			Debug.Log (i);
			Debug.Log (i / (cooltime * 10) * maxHeight);
			cooldownBar.sizeDelta = new Vector2(cooldownBar.rect.width, i / (cooltime*10) * maxHeight);
			yield return new WaitForSeconds (0.1f);
		}
		enabled = true;
		yield break;
	}
}

