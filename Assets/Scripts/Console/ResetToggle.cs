using UnityEngine;
public class ResetToggle : Toggle {
	public override void Up() {
		TransitionManager.ToMenu();
	}
}