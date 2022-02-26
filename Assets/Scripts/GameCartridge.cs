using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cartridge", menuName = "ScriptableObjects/GameCartridge")]
public class GameCartridge : ScriptableObject {
	[SerializeField]
	private GameId id;
	public GameId Id => id;

	[SerializeField]
	private ReadOnlyPalette<Color> colors;
	public IReadOnlyPalette<Color> ColorPalette => colors;

	[SerializeField]
	private Palette<AudioClip> sounds;

	[SerializeField]
	private Palette<GlitchyObject> objects;

	public IReadOnlyPalette<GlitchyObject> ObjectPalette => objects;
}