using UnityEngine;

public interface ISpriteSheet {
	Sprite Original { get; }

	Sprite Grey { get; }

	Sprite OriginalAt(int index);

	Sprite GreyAt(int index);

	Sprite OriginalAtOrNext(int index);

	Sprite GreyAtOrNext(int index);

	int Count { get; }
}