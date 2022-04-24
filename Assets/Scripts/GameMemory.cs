using System;
using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	private readonly System.Random rand = new System.Random();

	[SerializeField]
	private int capacity = 16;

	private bool loaded;

	public GameCartridge ActiveCartridge { get; private set; }

	public int Corruption { get; private set; }

	private ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}
	//memory[0] = "5555";
	[SerializeField]
	private SpriteSheet missingSpriteSheet;
	public SpriteSheet MissingSpriteSheet => missingSpriteSheet;

	private Palette<IMemorable> memory; // hex codes
	private List<IRefreshable> refreshMemory;

	private Palette<string> playerCodes; // player hex codes

	void Awake() {
		if( Instance != null )
			return;

		Instance = this;

		memory = new Palette<IMemorable>();
		for( int i = 0; i < capacity; ++i )
			memory.Add(new MemoryItem());

		InitializePlayerCodes();

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

		Clear();

		ActiveCartridge = GameCollection.Instance.Cartridge(gameId);
		ColorPalette = new ColorPalette(gameId);
		Debug.Log($"Game ID: {gameId}, Hexcode:{ColorPalette.ToHex}");
		// AudioPalette = new AudioPalette(GameId);

		for( int i = 0; i < ActiveCartridge.EntitiesPalette.Count; ++i )
			memory[i + 2] = ActiveCartridge.EntitiesPalette[i];

		ApplyCorruption(Corruption / 5);
		Refresh();
		loaded = true;
		Refresh();

	}

	void Clear() {
		refreshMemory.RemoveAll(x => !(x is HexConsole));
	}

	public IMemorable MemoryItem(int index) => memory[index];

	public IMemorable MemoryItem(string hex) {
		return MemoryItem(AddressOf(hex));
	}

	public Color Color(int index) => ColorPalette[index];

	public EntityData DynamicEntityData(string hex) {
		return StaticEntityData(MemoryItem(hex).ToHex);
	}

	public int AddressOf(string hex) {
		return HexToInt(hex.Substring(1, 1)) + 2;
	}

	public EntityData StaticEntityData(string hex) {
		int gameIndex = HexToInt(hex.Substring(0, 1));    //1XXX
		int entityIndex = HexToInt(hex.Substring(1, 1));  //X1XX
		var palette = GameCollection.Instance.Cartridge(gameIndex).EntitiesPalette;

		return palette[entityIndex];
	}

	public bool IsPlayer(string hex) {
		return playerCodes.Contains(hex);
	}

	public string PlayerHex(GameId id) {
		return playerCodes[(int)id];
	}

	public GameId GameOfPlayer(string hex) {
		return (GameId)playerCodes.IndexOf(hex);
	}

	private void InitializePlayerCodes() {
		playerCodes = new Palette<string>();
		for( int i = 0; i < Enum.GetValues(typeof(GameId)).Length - 1/*TODO: make into method to get game count*/; ++i ) {
			string currentHex = RandomHexString();
			while( playerCodes.Contains(currentHex) ) {
				currentHex = RandomHexString();
			}
			playerCodes.Add(currentHex);
			Debug.Log($"Player code {i} : {currentHex}");
		}
	}

	private void Refresh() {
		refreshMemory.RemoveAll((IRefreshable r) => r == null || !r.IsActive);
		foreach( var refreshItem in refreshMemory )
			if( refreshItem.IsActive )
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
		if( Input.GetKeyDown(KeyCode.G) ) {
			Corrupt();
		}
		if( Input.GetKeyDown(KeyCode.Comma) ) {
			memory[2] = new MemoryItem(playerCodes[0]); Refresh();
		}
		if( Input.GetKeyDown(KeyCode.Period) ) {
			memory[2] = new MemoryItem(playerCodes[1]); Refresh();
		}
		if( Input.GetKeyDown(KeyCode.Slash) ) {
			memory[2] = new MemoryItem(playerCodes[2]); Refresh();
		}
	}

	public void ChanceOfCorruption(double chance) {
		if( rand.NextDouble() <= chance )
			Corrupt();
	}

	public void Corrupt() {
		++Corruption;
		ApplyCorruption(1);
	}

	private void ApplyCorruption(int count = 1) {
		for( int i = 0; i < count; ++i ) {
			int random = rand.Next(capacity);
			memory[random] = new MemoryItem(RandomHexString());
		}

		Refresh();
	}

	private string RandomHexString() {
		string hex = "";

		for( int i = 0; i < 4; ++i )
			hex += RandomHexChar();

		return hex;
	}

	private char RandomHexChar() {
		int n = rand.Next(16);

		return (char)(n < 10 ? '0' + n : 'A' + n - 10);
	}
}