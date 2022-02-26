using UnityEngine;

public class MemoryItem : IMemorable {
	[SerializeField]
	private string hex;

	public MemoryItem() {
		hex = "0000";
	}

	public MemoryItem(string str) {
		hex = str;
	}

	public string ToHex => hex;

	public static implicit operator MemoryItem(string hex) {
		return new MemoryItem(hex);
	}
}