using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(
	fileName = nameof(ColorGamut),
	menuName = Paths.SCRIPTABLE_OBJECTS + "/" + nameof(ColorGamut))
]
public class ColorGamut : ScriptableObject {
	const int LUMA_COUNT = 4;

	[SerializeField]
	private Palette<Color> VeryDark;

	[SerializeField]
	private Palette<Color> Dark;

	[SerializeField]
	private Palette<Color> Medium;

	[SerializeField]
	private Palette<Color> Light;

	public Color Color(GlitchyHue hue, GlitchyLuma luma) => Color((int)hue, (int)luma);

	public Color Color(int hue, int luma) => (luma % LUMA_COUNT) switch {
		(int)GlitchyLuma.VeryDark => VeryDark[hue],
		(int)GlitchyLuma.Dark => Dark[hue],
		(int)GlitchyLuma.Medium => Medium[hue],
		(int)GlitchyLuma.Light => Light[hue],
		_ => VeryDark[(int)GlitchyHue.Black]
	};
}