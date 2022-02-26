using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	public GameId GameId { get; private set; }

	private bool loaded;

	public GameCartridge ActiveCartridge => GameCollection.Instance.Cartridge(GameId);

	public ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}

	[SerializeField]
	private Palette<AudioClip> sounds;

	[SerializeField]
	private Palette<GameObject> gameObjects;

	public GameObject GameObject(int index) => gameObjects[index].gameObject;

	private List<IMemorable> memory; // hex codes

	private List<IRefreshable> refreshMemory;

	void Awake() {
		DontDestroyOnLoad(this);

		Instance = this;
		memory = new List<IMemorable> { null, null };
		refreshMemory = new List<IRefreshable>();
	}

	public void Store(IRefreshable refreshItem) {
		refreshMemory.Add(refreshItem);
		if( loaded )
			refreshItem.Refresh();
	}

	public void Load(GameId gameId) {
		loaded = false;

		GameId = gameId;
		ColorPalette = new ColorPalette(GameId);
		// AudioPalette = new AudioPalette(GameId);
		for( int i = 0; i < ActiveCartridge.ObjectPalette.Count; ++i )
			memory[i + 2] = ActiveCartridge.ObjectPalette[i];

		Refresh();

		loaded = true;
	}

	public Color Color(GameId gameId, int index) {
		if( gameId != GameId )
			index += GameCollection.Instance.Cartridge(GameId).ColorPalette.Count;

		return ColorPalette[index];
	}

	public GlitchyObject GlitchyObject(string hex) {
		return ActiveCartridge.ObjectPalette[HexToInt(hex.Substring(0, 2))];
	}

	private void Refresh() {
		foreach( var refreshItem in refreshMemory )
			refreshItem.Refresh();
	}

	public static int HexToInt(string hex) {
		return Convert.ToInt32(hex, 16);
	}

	public static string IntToHex(int value) {
		return Convert.ToString(value, 16);
	}
}