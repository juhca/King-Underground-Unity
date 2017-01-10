using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
	[SerializeField]
	private Image bar;
	
	void Start() {

	}
	
	void Update() {
		//HandleBar();
	}

	public void HandleBar(float health) {
		bar.fillAmount = health / 100;
	}
}
