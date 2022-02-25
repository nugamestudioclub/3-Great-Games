using UnityEngine;
public class GameController : BaseGameController {
	public static GameController Instance { get; private set; }

	void Awake() {
		Instance = this;

		/*
		if( colorPalette == null )
			colorPalette = ScriptableObject.CreateInstance<ColorPalette>();
		if( audioPalette == null )
			audioPalette = ScriptableObject.CreateInstance<AudioPalette>();
		if( gameObjectPalette == null )
			gameObjectPalette = ScriptableObject.CreateInstance<GameObjectPalette>();
		*/
	}

	public void Load() {
		for( int i = 0; i < colorPalette.Count; ++i )
			if( colorPalette[i] == null )
				colorPalette[i] = MinigameController.Instance.Color(i);
	}
}