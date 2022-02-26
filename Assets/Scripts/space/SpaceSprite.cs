using UnityEngine;

public enum SpaceColor
{
	LightLavender,
	Lavender,
	Cyan,
	Red,
	LightPink,
	Pink,
	White,
	Green,
	Orange,
	Mauve,
	LightBlue,
	Blue,
	Yellow,
	Yellow2,
	Blue2,
	DarkBlue,
}

public class SpaceSprite : GlitchySprite
{
	[SerializeField]
	private SpaceColor color;

	protected override GameId GameId => GameId.SpaceShooter;

	public override int ColorId => (int)color;
}