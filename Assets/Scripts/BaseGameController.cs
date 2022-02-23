using System;
using UnityEngine;

public abstract class BaseGameController : MonoBehaviour {
	[SerializeField]
	protected ColorPalette colorPalette;

	[SerializeField]
	protected AudioPalette audioPalette;

	[SerializeField]
	protected GameObjectPalette gameObjectPalette;

	public Color Color(int index) => colorPalette[index];
	public Color Color(string hex) => Color(FromHex(hex));

	public AudioClip Audio(int index) => audioPalette[index];
	public AudioClip Audio(string hex) => Audio(FromHex(hex));

	public GameObject GameObject(int index) => gameObjectPalette[index];
	public GameObject GameObject(string hex) => GameObject(FromHex(hex));

	public static int FromHex(string hex) {
		return Convert.ToInt32(hex, 16);
	}

	public static string ToHex(int value) {
		return Convert.ToString(value, 16);
	}
}