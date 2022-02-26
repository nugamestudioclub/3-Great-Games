using UnityEngine;

[CreateAssetMenu(fileName = "Cartridge", menuName = "ScriptableObjects/GameCartridge")]
public class GameCartridge : ScriptableObject {
	[SerializeField]
	private GameId id;

	public GameId Id => id;

	[SerializeField]
	private Palette<Color> colors;
	public Palette<Color> ColorPalette => colors;

	[SerializeField]
	private Palette<AudioClip> sounds;

	[SerializeField]
	private Palette<GlitchyObject> gameObjects;
	//public GameObject GameObject(int index) => gameObjects[index].gameObject;

	//write instructions
	public string writeColorPalette()
    {
		return $"0{(int)(Id + 1)}00";
    }
	public string writeAudioPalette()
    {
							//tempo pitch
		return $"1{(int)(Id + 1)}00";
	}
	public string writeGameObjects()
	{
		for (int i = 0; i < gameObjects.Count; i++)
        {
			return writeGameObject(i, i, i);
        }
		return "";
	}
	public string writeGameObject(int gameObjectId, int colorId, int audioId)
    {
		return $"2{gameObjectId}{colorId}{audioId}";

	}
	//read instruction
}