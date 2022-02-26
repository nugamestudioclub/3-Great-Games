using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	public GameId GameId { get; private set; }

	private bool loaded;

	public ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}

	[SerializeField]
	private Palette<AudioClip> sounds;

	[SerializeField]
	private Palette<GameObject> gameObjects;

	private List<IMemorable> memory;

	void Awake() {
		DontDestroyOnLoad(this);

		Instance = this;
		memory = new List<IMemorable>();
		memory.Add(null);
	}

	public void Store(IMemorable memoryItem) {
		memory.Add(memoryItem);
		if( loaded && memoryItem is IRefreshable refreshment )
			refreshment.Refresh();
	}

	public void Load(GameId gameId) {
		loaded = false;

		GameId = gameId;
		ColorPalette = new ColorPalette(GameId);
		Refresh();

		loaded = true;
	}

	public Color Color(GameId gameId, int index) {
		if( gameId != GameId )
			index += GameCollection.Instance.Cartridge(GameId).ColorPalette.Count;

		return ColorPalette[index];
	}

	private void Refresh() {
		foreach( var memoryItem in memory )
			if( memoryItem is IRefreshable refreshment )
				refreshment.Refresh();
	}

	private void LoadColors(GameCartridge gameCartridge) {
		/*
		for (int i = 0, offset = ColorCount - gameCartridge.ColorCount; i < offset; ++i)
			colors[i + offset] = colors[i];
		for (int i = 0; i < gameCartridge.ColorCount; ++i)
			colors[i] = gameCartridge.Color(i);
		*/
	}

	public static int HexToInt(string hex) {
		return Convert.ToInt32(hex, 16);
	}

	public static string IntToHex(int value) {
		return Convert.ToString(value, 16);
	}
}