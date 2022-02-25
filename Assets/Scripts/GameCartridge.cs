using UnityEngine;

public abstract class GameCartridge : ScriptableObject {
	public abstract GameId Id { get; }

	[SerializeField]
	private Palette<Color> colors;

	public Color Color(int index) => colors[index];

	public int ColorCount => colors.Count;
}