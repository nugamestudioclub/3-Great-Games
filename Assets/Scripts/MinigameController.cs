using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameController : MonoBehaviour {
	[SerializeField]
	protected ColorPalette colorPalette;

	[SerializeField]
	protected AudioPalette audioPalette;

	[SerializeField]
	protected GameObjectPalette gameObjectPalette;

	public static MinigameController Instance { get; private set; }

	void Awake() {
		Instance = this;
	}

	public Color Color(int index) => colorPalette[index];
	public Color Color(string hex) => Color(FromHex(hex));

	public AudioClip Audio(int index) => audioPalette[index];
	public AudioClip Audio(string hex) => Audio(FromHex(hex));

	public GameObject GameObject(int index) => gameObjectPalette[index];
	public GameObject GameObject(string hex) => GameObject(FromHex(hex));

	//023935 -1
	//120499 -2
	//393933 -3
	//coin -4

	public static int FromHex(string hex) {
		return Convert.ToInt32(hex, 16);
	}

	public static string ToHex(int value) {
		return Convert.ToString(value, 16);
	}
	/// <summary>
	/// Appends all objects that this game uses to the cache
	/// </summary>
	public abstract void WriteToCache(GameCache cache);

	/// <summary>
	/// Reads 
	/// </summary>
	/// <param name="cache"></param>
	public abstract void ReadCache(GameCache cache);

	/// <summary>
	/// Instantiate all objects at their coordinates
	/// </summary>
	public abstract void LoadGame();
}
