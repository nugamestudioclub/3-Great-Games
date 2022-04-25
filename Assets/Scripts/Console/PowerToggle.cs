using UnityEngine;
public class PowerToggle : Toggle {
	public override void Up() {
		Application.Quit();
	}
}