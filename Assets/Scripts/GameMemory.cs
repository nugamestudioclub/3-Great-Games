using System.Collections.Generic;
using UnityEngine;

public class GameMemory : MonoBehaviour {
	public static GameMemory Instance { get; private set; }

	public GameCartridge GameCartridge { get; private set; }

	private bool loaded;

	[SerializeField]
	private Palette<Color> colors;

	[SerializeField]
	private Palette<AudioClip> sounds;

	[SerializeField]
	private Palette<GameObject> gameObjects;

	private List<IMemorable> memory;

	void Awake() {
		Instance = this;
		memory = new List<IMemorable>();
	}

	public void Store(IMemorable memoryItem) {
		memory.Add(memoryItem);
		Debug.Log("Storing obj");
		if( loaded && memoryItem is IRefreshable refreshment)
			refreshment.Refresh();
	}

	public void Load(GameCartridge gameCartridge) {
		loaded = false;

		LoadColors(gameCartridge);
		GameCartridge = gameCartridge;
		Refresh();

		loaded = true;
	}

	public Color Color(GameId gameId, int index) {
		if( gameId != GameCartridge.Id )
			index += GameCartridge.ColorCount;

		return colors[index];
	}

	public int ColorCount => colors.Count;

	private void Refresh() {
		foreach( var memoryItem in memory )
			if (memoryItem is IRefreshable refreshment)
				refreshment.Refresh();
	}

	private void LoadColors(GameCartridge gameCartridge) {
		for (int i = 0, offset = ColorCount - gameCartridge.ColorCount; i < offset; ++i)
			colors[i + offset] = colors[i];
		for (int i = 0; i < gameCartridge.ColorCount; ++i)
			colors[i] = gameCartridge.Color(i);
	}
}