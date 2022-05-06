using UnityEngine;

public interface ISpriteSheet {
	Sprite Original { get; }

	Sprite Grey { get; }

	Sprite OriginalAt(int index);

	Sprite GreyAt(int index);

	Sprite FindUniqueOriginal(int start);

	Sprite FindUniqueGrey(int start);
}