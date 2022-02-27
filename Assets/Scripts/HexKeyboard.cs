using static System.Uri;
using UnityEngine;
using UnityEngine.UI;

public class HexKeyboard : MonoBehaviour {
	private InputField input;

	[SerializeField]
	private int maxLength = 4;

	public int Id { set; get; }

	public string Text {
		get => input.text;
		set => input.text = value;
	}

	private void Awake() {
		input = GetComponent<InputField>();
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
	}
}