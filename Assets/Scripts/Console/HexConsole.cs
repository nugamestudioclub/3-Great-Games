﻿using UnityEngine;

public class HexConsole : MonoBehaviour, IRefreshable {
	[SerializeField]
	private HexKeyboard[] keyboards = new HexKeyboard[16];

	public bool IsActive => true;

	public static HexConsole Instance { get; private set; }
	void Awake() {
		if( Instance != null )
			Destroy(gameObject);

		Instance = this;
	}

	void Start() {
		for( int i = 0; i < keyboards.Length; ++i )
			keyboards[i].Id = i;
		GameMemory.Instance.Subscribe(this);
	}

	public void Refresh() {

		for( int i = 0; i < keyboards.Length; ++i )
			Debug.Log($"keyboard {i} is null: {keyboards[i] == null } ");
		for( int i = 0; i < keyboards.Length; ++i )
			keyboards[i].Text = GameMemory.Instance.MemoryItem(i).ToHex;
	}
}