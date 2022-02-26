using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : IMemorable, IEnumerable<Color>, IReadOnlyList<Color> {
	private readonly GameId id;

	private IReadOnlyPalette<Color> Palette => GameCollection.Instance.Cartridge(id).ColorPalette;

	public ColorPalette(GameId id) {
		this.id = id;
	}

	public Color this[int index] => Palette[index];

	public int Count => Palette.Count;

	public IEnumerator<Color> GetEnumerator() {
		return Palette.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return Palette.GetEnumerator();
	}

	public static ColorPalette Get(GameId gameId) {
		return new ColorPalette(gameId);
	}

	public static ColorPalette FromHex(string hex) {
		Debug.Log(hex);
		return new ColorPalette(GameCollection.Instance.GameId(GameMemory.HexToInt(hex.Substring(1, 1))));
	}

	public string ToHex => $"E{GameMemory.IntToHex((int)id)}00";
}