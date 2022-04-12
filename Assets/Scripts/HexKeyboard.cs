using static System.Uri;
using UnityEngine;
using UnityEngine.UI;
using System;

public class HexKeyboard : MonoBehaviour {
	private InputField input;
	private Image image;
	[SerializeField]
	private int maxLength = 4;

	public int Id { set; get; }

	[SerializeField]
	private Color baseColor = Color.yellow;

	[SerializeField]
	private Color altColor = Color.red;

	private Sprite Icon {
		get {
			return Id switch {
				0 => GameCartridge.FromHex(Text).ColorPaletteSprite,
				_ => GameMemory.Instance.StaticEntityData(Text).SpriteSheet.Grey
			};
		}
	}

	public string Text {
		get => input.text;
		set {
			value = value.ToUpper();
			ChangeTextColor(input.text == value ? baseColor : altColor);
			input.text = value;
			image.sprite = Icon;
		}
	}


	private void ChangeTextColor(Color color) {
		input.textComponent.color = color;
	}

	private void Awake() {
		input = GetComponentInChildren<InputField>();
		image = GetComponentInChildren<Image>();
	}

	void Start() {
		input.onValidateInput += Validate;
		input.onEndEdit.AddListener(delegate { Submit(); });
	}

	private char Validate(string text, int pos, char ch) {
		return text.Length < 4 && IsHexDigit(ch) ? char.ToUpper(ch) : '\0';
	}

	private void Submit() {
		int count = maxLength - Text.Length;

		if( count > 0 )
			Text += new string('0', maxLength - Text.Length);
		GameMemory.Instance.Store(Id, new MemoryItem(Text));
		image.sprite = Icon;
	}
}