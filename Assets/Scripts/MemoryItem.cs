using UnityEngine;

public class MemoryItem : IMemorable {
	[SerializeField]
	private string hex;

	private const string empty = "FFFF";

	public static readonly MemoryItem Empty = new MemoryItem(empty);

	public string ToHex => hex;

	public MemoryItem(string str) {
		hex = str;
	}

	public static implicit operator MemoryItem(string hex) {
		return new MemoryItem(hex);
	}
}