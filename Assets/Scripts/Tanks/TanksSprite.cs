using UnityEngine;

public enum TanksColor {
	A,
	B,
	C,
	D,
	E,
	F,
}

public class TanksSprite : GlitchySprite {
	[SerializeField]
	private TanksColor colorId;

	protected override GameId GameId => GameId.Tanks;

	public override int ColorId => (int)colorId;
}