using UnityEngine;

public enum PlatformerColor {
	Green,
	Purple,
}

public class PlatformerSprite : GlitchySprite {
	[SerializeField]
	private PlatformerColor color;

	protected override GameId GameId => GameId.Platformer;

	public override int ColorId => (int)color;
}