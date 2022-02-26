using UnityEngine;

public class HexConsole : MonoBehaviour {
	[SerializeField]
	private HexKeyboard[] keyboards = new HexKeyboard[16];

	void Start() {
		for( int i = 0; i < keyboards.Length; ++i )
			keyboards[i].Id = i;
	}
}