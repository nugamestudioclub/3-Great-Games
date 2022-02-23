public abstract class MinigameController : BaseGameController {
	public static MinigameController Instance { get; private set; }

	void Awake() {
		Instance = this;
	}

	void Start() {
		UnityEngine.Debug.Log("start");
		GameController.Instance.Load();
	}

	/// <summary>
	/// Appends all objects that this game uses to the cache
	/// </summary>
	public abstract void WriteToCache(GameCache cache);

	/// <summary>
	/// Reads 
	/// </summary>
	/// <param name="cache"></param>
	public abstract void ReadCache(GameCache cache);

	/// <summary>
	/// Instantiate all objects at their coordinates
	/// </summary>
	public abstract void LoadGame();
}
