using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	[SerializeField]
	private int capacity = 16;

	private bool loaded;

	public GameCartridge ActiveCartridge { get; private set; }

	public ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}

	[SerializeField]
	private Palette<GlitchyObject> objects;
	public GlitchyObject Object(int index) => objects[index];

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

	public GlitchyObject Object(string hex) {
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