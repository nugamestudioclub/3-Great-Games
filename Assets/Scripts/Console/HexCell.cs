using UnityEngine;
using UnityEngine.UI;

public class HexCell : MonoBehaviour {

	[HideInInspector]
	public HexKeyboard keyboard;
	[HideInInspector]
	public Image image;

	public int Id { set; get; }

	void Awake() {
		keyboard = GetComponentInChildren<HexKeyboard>();
		image = GetComponentInChildren<Image>();
	}

	public void UpdateCell(string text, Sprite sprite) {
		keyboard.Text = text;
		image.sprite = sprite;	
	}
}