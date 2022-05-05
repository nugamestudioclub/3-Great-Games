using UnityEngine;

public abstract class SpriteSheet : ScriptableObject, ISpriteSheet {
	public abstract Sprite Original { get; }

	public abstract Sprite Grey { get; }

    public abstract Sprite OriginalAt(int index);
    
    public abstract Sprite GreyAt(int index);

    public abstract Sprite OriginalAtOrNext(int index);

    public abstract Sprite GreyAtOrNext(int index);
}