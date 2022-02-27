using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	[SerializeField]
	private int capacity = 16;

	private bool loaded;

	public GameCartridge ActiveCartridge { get; private set; }

	private ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}

	[SerializeField]
	private Sprite missingSprite;
	public Sprite MissingSprite => missingSprite;

	private Palette<IMemorable> memory; // hex codes

	private List<IRefreshable> refreshMemory;

	void Awake() {
		DontDestroyOnLoad(this);

		Instance = this;

		memory = new Palette<IMemorable>();
		for( int i = 0; i < capacity; ++i )
			memory.Add(new MemoryItem());

		refreshMemory = new List<IRefreshable>();
	}

	public void Store(int index, IMemorable memoryItem) {
		memory[index] = memoryItem;
		Refresh();
	}

	public void Subscribe(IRefreshable refreshItem) {
		refreshMemory.Add(refreshItem);
		if( loaded )
			refreshItem.Refresh();
	}

	public void Load(GameId gameId) {
		loaded = false;

		ActiveCartridge = GameCollection.Instance.Cartridge(gameId);
		ColorPalette = new ColorPalette(gameId);
		// AudioPalette = new AudioPalette(GameId);

		for( int i = 0; i < ActiveCartridge.ObjectPalette.Count; ++i )
			memory[i + 2] = ActiveCartridge.ObjectPalette[i];
		Refresh();

		loaded = true;
	}

	public IMemorable MemoryItem(int index) => memory[index];

	public Color Color(int index) => ColorPalette[index];

	public GlitchyObject Object(string hex) {
		int memoryIndex = HexToInt(hex.Substring(1, 1)) + 2;
		string memoryHex = memory[memoryIndex].ToHex;
		int objIndex = HexToInt(memoryHex.Substring(1, 1));
		int gameIndex = HexToInt(memoryHex.Substring(0, 1));

		return GameCollection.Instance.Cartridge(gameIndex).ObjectPalette[objIndex];
	}

	private void Refresh() {
		foreach( var refreshItem in refreshMemory )
			if( refreshItem.IsActive )
				refreshItem.Refresh();
		refreshMemory.RemoveAll((IRefreshable r) => r == null || !r.IsActive);
	}

	public static int HexToInt(string hex) {
		return Convert.ToInt32(hex, 16);
	}

	public static string IntToHex(int value) {
		return Convert.ToString(value, 16);
	}
}