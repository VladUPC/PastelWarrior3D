using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{

	[SerializeField] private ProgressBar healthBar;
	[SerializeField] private WeaponSlot firstSlot;
	[SerializeField] private WeaponSlot secondSlot;
	public GameObject slot;

	public void UpdateHealth(int health, int bhealth, int shield, int bshield) {
		healthBar.SetValues(health, bhealth, shield, bshield);
	}

	public void SetAmmo(bool first, int value) {
		if (first) firstSlot.SetAmmo(value);
		else secondSlot.SetAmmo(value);
	}

	public void SetActiveWeapon(bool first) {
		if (first) {
			firstSlot.Activate();
			secondSlot.Deactivate();
		}
		else {
			Debug.Log("Switch to weapon 2");
			secondSlot.Activate();
			firstSlot.Deactivate();
		}
	}

	public void SecondWeaponFound() {
		slot.SetActive(true);
	}


}
