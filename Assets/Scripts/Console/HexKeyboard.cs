using static System.Uri;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HexKeyboard : MonoBehaviour {
	[SerializeField]
	private TMP_InputField input;

	[SerializeField]
	private Image image;

	[SerializeField]
	private int maxLength = 4;

	public int Id { set; get; }

	[SerializeField]
	private Color baseColor = new Color(0.5f, 1.0f, 0.0f, 1.0f);

	[SerializeField]
	private Color altColor = Color.red;

	private Graphic graphic;

	private Sprite Icon {
		get {
			return Id switch {
				0 => GameCartridge.FromHex(Text).ColorPaletteSprite,
				_ => GameMemory.Instance.StaticEntityData(Text).SpriteSheet.Grey
			};
		}
	}

	private string text;
	public string Text {
		get => input.text;
		set {
			value = value.ToUpper();
			
			Tint(value == text ? baseColor : altColor);

			text = value;
			input.text = value;
			image.sprite = Icon;
		}
	}

	void Awake() {
		graphic = image.GetComponent<Graphic>();

		input.placeholder.color = baseColor;
		Tint(baseColor);
	}

	void Start() {
		input.onValidateInput += Validate;
		input.onEndEdit.AddListener(delegate { Submit(); });
	}

	private void Tint(Color color) {
		input.textComponent.color = color;
		input.textComponent.fontSharedMaterial.SetColor(ShaderUtilities.ID_GlowColor, color);
		if( Id != 0 )
			graphic.color = color;
	}

	private char Validate(string text, int pos, char ch) {
		return text.Length < 4 && IsHexDigit(ch) ? char.ToUpper(ch) : '\0';
	}

	private void Submit() {
		int count = maxLength - input.text.Length;

		if( count > 0 )
			input.text += new string('0', count);
		GameMemory.Instance.Store(Id, new MemoryItem(input.text));
	}
}