using UnityEngine;

[CreateAssetMenu(
	fileName = nameof(GameCartridge),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(GameCartridge))
]
public class GameCartridge : ScriptableObject {
	[SerializeField]
	private GameId id;
	public GameId Id => id;

	[SerializeField]
	private ReadOnlyPalette<Color> colors;
	public IReadOnlyPalette<Color> ColorPalette => colors;

	[field: SerializeField]
	public Sprite ColorPaletteSprite { get; set; }

	[SerializeField]
	private ReadOnlyPalette<AudioClip> sounds;
	
	//[SerializeField]
	//private ReadOnlyPalette<GlitchyObject> objects;

	//public IReadOnlyPalette<GlitchyObject> ObjectPalette => objects;
	
	[SerializeField]
	private ReadOnlyPalette<EntityData> entities;

	public IReadOnlyPalette<EntityData> EntitiesPalette => entities;

	public static GameCartridge FromHex(string hex) {
		return GameCollection.Instance.Cartridge(GameMemory.HexToInt(hex.Substring(2, 1)));
	}
}