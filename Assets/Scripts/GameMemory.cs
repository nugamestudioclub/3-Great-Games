using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	public System.Random Rand { get; private set; } = new System.Random();

	[SerializeField]
	private int capacity = 16;

	private bool loaded;

	public GameCartridge ActiveCartridge { get; private set; }

	public int Corruption { get; private set; }

	private ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}

	[SerializeField]
	private SpriteSheet missingSpriteSheet;
	public SpriteSheet MissingSpriteSheet => missingSpriteSheet;

	private Palette<IMemorable> memory; // hex codes

	private List<IRefreshable> refreshMemory;

	void Awake() {
		if (Instance != null) {
			Destroy(this);
		}
		else {
			DontDestroyOnLoad(this);
			Instance = this;
			memory = new Palette<IMemorable>();
			for (int i = 0; i < capacity; ++i)
				memory.Add(new MemoryItem());

			refreshMemory = new List<IRefreshable>();
		}
	}

	public void Store(int index, IMemorable memoryItem) {
		memory[index] = memoryItem;
		Refresh();
	}

	public void Subscribe(IRefreshable refreshItem) {
		refreshMemory.Add(refreshItem);
		if (loaded)
			refreshItem.Refresh();
	}

	public void Load(GameId gameId) {
		loaded = false;

		refreshMemory.Clear();

		ActiveCartridge = GameCollection.Instance.Cartridge(gameId);
		ColorPalette = new ColorPalette(gameId);
		// AudioPalette = new AudioPalette(GameId);

		for (int i = 0; i < ActiveCartridge.EntitiesPalette.Count; ++i)
			memory[i + 2] = ActiveCartridge.EntitiesPalette[i];
		
		ApplyCorruption(Corruption % 5 + 1);
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
		//var objPalette = GameCollection.Instance.Cartridge(gameIndex % GameCollection.Instance.Count).ObjectPalette;

		return null;// objPalette[objIndex % objPalette.Count];
	}

	public EntityData EntityData(string hex) {
		int memoryIndex = HexToInt(hex.Substring(1, 1)) + 2;
		string memoryHex = memory[memoryIndex].ToHex;
		int objIndex = HexToInt(memoryHex.Substring(1, 1));
		int gameIndex = HexToInt(memoryHex.Substring(0, 1));
		var objPalette = GameCollection.Instance.Cartridge(gameIndex % GameCollection.Instance.Count).EntitiesPalette;

		return objPalette[objIndex % objPalette.Count];
	}

	private void Refresh() {
		refreshMemory.RemoveAll((IRefreshable r) => r == null || !r.IsActive);
		foreach (var refreshItem in refreshMemory)
			if (refreshItem.IsActive)
				refreshItem.Refresh();
		
	}

	public static int HexToInt(string hex) {
		return Convert.ToInt32(hex, 16);
	}

	public static string IntToHex(int value) {
		return Convert.ToString(value, 16);
	}

	/// <summary>
	/// TODO REMOVE THIS
	/// </summary>
	private void Update() {
		if (Input.GetKeyDown(KeyCode.C)) {
			Corrupt();

		}
	}
	public void Corrupt() {
		++Corruption;
		ApplyCorruption(1);
	}

	private void ApplyCorruption(int count = 1) {
		for (int i = 0; i < count; ++i)
			memory[Rand.Next(capacity)] = new MemoryItem(RandomHexString());
		Debug.Log("Corrupting...");
		Refresh();
	}

	private string RandomHexString() {
		string hex = "";

		for (int i = 0; i < 4; ++i)
			hex += RandomHexChar();

		return hex;
	}

	private char RandomHexChar() {
		int n = Rand.Next(16);

		return (char)(n < 10 ? '0' + n : 'A' + n - 10);
	}
}