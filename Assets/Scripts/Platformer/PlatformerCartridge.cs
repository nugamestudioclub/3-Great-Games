using UnityEngine;

[CreateAssetMenu(fileName = "PlatformerCartridge", menuName = "ScriptableObjects/Cartridge/Platformer")]
public class PlatformerCartridge : GameCartridge {
	public override GameId Id => GameId.Platformer;
}