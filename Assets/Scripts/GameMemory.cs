using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMemory : MonoBehaviour {

	public static GameMemory Instance { get; private set; }

	private readonly System.Random rand = new System.Random();

	[SerializeField]
	private int capacity = 16;

	private bool loaded;

	public bool IsActive { get; private set; }
	public GameCartridge ActiveCartridge { get; private set; }

	[field: ReadOnly]
	[field: SerializeField]
	public float Corruption { get; private set; }
	[SerializeField]
	private float corruptionFactorPercent = .10f;
	[SerializeField]
	private float corruptionIncremenetFactor = .33f;

	private ColorPalette ColorPalette {
		get => ColorPalette.FromHex(memory[0].ToHex);
		set => memory[0] = value;
	}

	[SerializeField]
	private SpriteSheet missingSpriteSheet;
	public SpriteSheet MissingSpriteSheet => missingSpriteSheet;

	private Palette<IMemorable> memory; // hex codes

	private List<IRefreshable> refreshMemory;

	[SerializeField]
	private Hint hint;

	[ReadOnly]
	[SerializeField]
	private Palette<string> playerCodes; // player hex codes

	private Palette<bool> gamesWon = new Palette<bool>(GameCollection.Count);

	void Awake() {
		if( Instance != null )
			return;

		Instance = this;


		InitializeMemoryItems();
		InitializePlayerCodes();
		InitializeGamesWon();

		Load(GameId.None);
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

		if( gameId == GameId.None ) {
			ActiveCartridge = null;
			for( int i = 0; i < memory.Count; ++i )
				memory[i] = MemoryItem.Empty;
			IsActive = false;
		}
		else {
			ActiveCartridge = GameCollection.Instance.Cartridge(gameId);
			ColorPalette = new ColorPalette(gameId);
			// Debug.Log($"Game ID: {gameId}, Hexcode:{ColorPalette.ToHex}");
			// AudioPalette = new AudioPalette(GameId);

			for( int i = 0; i < ActiveCartridge.EntitiesPalette.Count; ++i )
				memory[i + 2] = ActiveCartridge.EntitiesPalette[i];

			IsActive = true;
			Refresh();
			ChanceOfCorruption(Corruption);
			Refresh();
		}

		loaded = true;

	}

	void Clear() {
		refreshMemory.RemoveAll(x => !(x is HexConsole));
	}

	public IMemorable At(int index) => memory[index];

	public IMemorable At(string hex) {
		return At(AddressOf(hex));
	}

	public Color Color(int index) => ColorPalette[index];

	public int AddressOf(string hex) {
		return HexToInt(hex.Substring(1, 1)) + 2;
	}

	public Entity DynamicEntity(string hex) {
		return StaticEntity(At(hex).ToHex);
	}

	public Entity StaticEntity(string hex) {
		int gameIndex = HexToInt(hex.Substring(0, 1));    //1XXX
		int entityIndex = HexToInt(hex.Substring(1, 1));  //X1XX
		var palette = GameCollection.Instance.Cartridge(gameIndex).EntitiesPalette;

		return palette[entityIndex];
	}
	public EntityData DynamicEntityData(string hex) {
		return StaticEntityData(At(hex).ToHex);
	}
	public EntityData StaticEntityData(string hex) {
		int gameIndex = HexToInt(hex.Substring(0, 1));    //1XXX
		int entityIndex = HexToInt(hex.Substring(1, 1));  //X1XX
		var palette = GameCollection.Instance.Cartridge(gameIndex).EntitiesPalette;

		return palette[entityIndex].Template;
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

	public bool HasWonGame(GameId gameId) => gamesWon[(int)gameId];

	public void WinGame(GameId gameId) {
		gamesWon[(int)gameId] = true;
	}

	public static void RevealPlatformerHint() {
		Instance.hint.Reveal(GameId.Platformer);
	}

	public static void RevealTanksHint() {
		Instance.hint.Reveal(GameId.Tanks);
	}

	public static void RevealSpaceShooterHint() {
		Instance.hint.Reveal(GameId.SpaceShooter);
	}

	public bool AllGamesWon() => !gamesWon.Contains(false);
	private void InitializeMemoryItems() {
		memory = new Palette<IMemorable>();
		for( int i = 0; i < capacity; ++i )
			memory.Add(MemoryItem.Empty);
		refreshMemory = new List<IRefreshable>();
	}
	private void InitializePlayerCodes() {
		playerCodes = new Palette<string>();
		for( int i = 0; i < GameCollection.Count/*TODO: make into method to get game count*/; ++i ) {
			string currentHex = RandomHexString();
			while( playerCodes.Contains(currentHex) ) {
				currentHex = RandomHexString();
			}
			playerCodes.Add(currentHex);
			Debug.Log($"Player code {i} : {currentHex}");
		}
	}

	private void InitializeGamesWon() {
		for( int i = 0; i < GameCollection.Count; ++i )
			gamesWon.Add(false);
	}

	private void Refresh() {
		refreshMemory.RemoveAll((IRefreshable r) => r == null || !r.IsActive);

		foreach( var memoryItem in refreshMemory )
			if( memoryItem.IsActive )
				memoryItem.Refresh();
		if( Zone.Instance != null ) {
			TilemapCollider2D tilemapCollider = Zone.Instance.Tilemap.GetComponent<TilemapCollider2D>();
			tilemapCollider.ProcessTilemapChanges();
			//Zone.Instance.Tilemap.enabled = false;
			//Zone.Instance.Tilemap.enabled = true;
		}
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
		/*
        if (Input.GetKeyDown(KeyCode.G))
        {
            Corrupt();
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            memory[2] = new MemoryItem(playerCodes[0]); Refresh();
        }
        if (Input.GetKeyDown(KeyCode.Period))
        {
            memory[2] = new MemoryItem(playerCodes[1]); Refresh();
        }
        if (Input.GetKeyDown(KeyCode.Slash))
        {
            memory[2] = new MemoryItem(playerCodes[2]); Refresh();
        }
        if (Input.GetKeyDown(KeyCode.Semicolon))
        {
            GlobalVolume.Instance.DecreaseVolume();
        }
        if (Input.GetKeyDown(KeyCode.Quote))
        {
            GlobalVolume.Instance.IncreaseVolume();
        }
        */
	}

	public void ChanceOfCorruption(float chance) {
		Corruption += chance * corruptionIncremenetFactor;
		if( rand.NextDouble() <= chance )
			ApplyCorruption(rand.Next((int)(Corruption * corruptionFactorPercent) + 1) + 1);
	}

	public void Corrupt() {
		ChanceOfCorruption(1f);
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