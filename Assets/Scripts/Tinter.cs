using UnityEngine;

public static class Tinter {
	public static byte Clamp(this byte value, byte min, byte max) {
		return value < min ? min : value > max ? max : value;
	}

	private const float lumaR = 0.2126f;
	private const float lumaG = 0.7152f;
	private const float lumaB = 0.0722f;

	public static float Luma(float r, float g, float b) {
		return Mathf.Clamp(lumaR * r + lumaG * g + lumaB * b, 0.0f, 1.0f);
	}

	public static byte Luma(byte r, byte g, byte b) {
		byte luma = (byte)(lumaR * r + lumaG * g + lumaB * b);

		return luma.Clamp(0, 1);
	}

	public static Color Greyscale(Color color) {
		float luma = (float)Luma(color.r, color.g, color.b);

		return new Color(luma, luma, luma);
	}

	public static Color32 Greyscale(Color32 color) {
		byte luma = Luma(color.r, color.g, color.b);

		return new Color32(luma, luma, luma, 255);
	}

	public static Texture2D Greyscale(Texture2D texture) {
		var greyscale = new Texture2D(texture.width, texture.height, TextureFormat.ARGB32, false);

		for( int x = 0; x < texture.width; ++x )
			for( int y = 0; y < texture.height; ++y )
				texture.SetPixel(x, y, Greyscale(texture.GetPixel(x, y)));
		return greyscale;
	}

	public static void Desaturate(Texture2D texture) {
		for( int x = 0; x < texture.width; ++x )
			for( int y = 0; y < texture.height; ++y ) {
				texture.SetPixel(x, y, Greyscale(texture.GetPixel(x, y)));
			}
	}
}