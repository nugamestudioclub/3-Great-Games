using UnityEngine;

public class HexConsole : MonoBehaviour, IRefreshable {
	[SerializeField]
	private HexKeyboard[] keyboards = new HexKeyboard[16];
    private void Awake()
    {
		DontDestroyOnLoad(this);
    }
    void Start() {
		for( int i = 0; i < keyboards.Length; ++i )
			keyboards[i].Id = i;
		GameMemory.Instance.Subscribe(this);
	}

	public void Refresh() {
		for( int i = 0; i < keyboards.Length; ++i )
			keyboards[i].Text = GameMemory.Instance.MemoryItem(i).ToHex;
	}
}