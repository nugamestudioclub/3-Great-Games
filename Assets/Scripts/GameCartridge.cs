using UnityEngine;

public class GameCartridge : ScriptableObject {
	[SerializeField]
	private GameId id;

	public GameId Id => id;

	[SerializeField]
	private Palette<Color> colors;

	public Color Color(int index) => colors[index];

	public int ColorCount => colors.Count;

	[SerializeField]
	private Palette<AudioClip> sounds;

	public AudioClip Sound(int index) => sounds[index];

	public int SoundCount => colors.Count;

	[SerializeField]
	private Palette<GameObject> gameObjects;

	public GameObject GameObject(int index) => gameObjects[index];

	public int GameObjectCount => colors.Count;
}