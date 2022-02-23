using UnityEngine;

public enum PlatformerColor {
	Green,
	Purple,
}

public class PlatformerSprite : GlitchySprite {
	[SerializeField]
	private PlatformerColor color;

	public override int ColorId => (int)color;
}