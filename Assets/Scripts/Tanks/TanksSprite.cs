using UnityEngine;

public enum TanksColor {
	Brown,
	DarkGreen,
	LightBlue,
	Yellow,
	Green,
	DarkBlue,
	Orange,
	H,
	I,
	J,
	K,
	L,
	M,
	N,
	O,
}

public class TanksSprite : GlitchySprite {
	[SerializeField]
	private TanksColor colorId;

	protected override GameId GameId => GameId.Tanks;

	public override int ColorId => (int)colorId;
}