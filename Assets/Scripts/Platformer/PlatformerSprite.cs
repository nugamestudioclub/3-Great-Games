using UnityEngine;

public enum PlatformerColor {
	Purple,
	MedGreen,
	DarkOrange,
	MedOrange,
	DarkGreen,
	LightGreen,
	Crimson,
	LightOrange,
	MedBlue,
	Brown,
	MedOrange2,
	LightOrange2,
	DarkBlue,
	Lavender,
	LightOrange3,
	Yellow,
}

public class PlatformerSprite : GlitchySprite {
	[SerializeField]
	private PlatformerColor color;

	protected override GameId GameId => GameId.Platformer;

	public override int ColorId => (int)color;
}