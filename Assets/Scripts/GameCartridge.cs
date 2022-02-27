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
	private ReadOnlyPalette<AudioClip> sounds;

	[SerializeField]
	private ReadOnlyPalette<GlitchyObject> objects;

	public IReadOnlyPalette<GlitchyObject> ObjectPalette => objects;
}