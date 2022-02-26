using static System.Uri;
using UnityEngine;
using UnityEngine.UI;

public class HexKeyboard : MonoBehaviour {
	private InputField input;

	public int Id { set; get; }

	void Start() {
		input.onValidateInput += Validate;
		input.onEndEdit.AddListener(delegate { Submit(); });
	}

	private char Validate(string text, int pos, char ch) {
		return text.Length <= 4 && IsHexDigit(ch) ? ch : '\0';
	}

	private void Submit() {
		GameMemory.Instance.Store(Id, new MemoryItem(input.text));
	}
}