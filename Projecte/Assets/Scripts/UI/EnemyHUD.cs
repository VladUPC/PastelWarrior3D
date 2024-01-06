using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHUD : MonoBehaviour
{

	[SerializeField] private EnemyLifeBar healthBar;
	private float elapsedTime = 0;
	public float showTime = 2f;
	public GameObject lifeBar;
	public bool show = false;

	
	public void UpdateHealth(int health, int bhealth, int shield, int bshield) {
		elapsedTime = 0;
		lifeBar.SetActive(true);
		healthBar.SetValues(health, bhealth, shield, bshield);
	}

	public void Update() {
		elapsedTime += Time.deltaTime;
		if (elapsedTime > showTime && !show) lifeBar.SetActive(false);
	}
	
	public void Hide() {
		if (!show) lifeBar.SetActive(false);
	}

	public void Deactivate() {
		lifeBar.SetActive(false);
	}


}
