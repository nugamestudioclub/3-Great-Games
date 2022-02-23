﻿using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class GlitchySprite : MonoBehaviour {
	private Color color;

	private SpriteRenderer spriteRenderer;
	private Sprite mainSprite;
	private Sprite greySprite;

	public Sprite Sprite { get => spriteRenderer.sprite; private set => spriteRenderer.sprite = value; }

	public bool IsTinted => Sprite == greySprite;

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		mainSprite = spriteRenderer.sprite;
		greySprite = GreySprite(mainSprite);
	}

	void Start() {
		color = MinigameController.Instance.Color(ColorId);
		Tint(GameController.Instance.Color(ColorId));
	}

	public void Tint(Color color) {
			Debug.Log(gameObject.name);
		if( color == this.color ) {
			Debug.Log("if");
			spriteRenderer.sprite = mainSprite;
			spriteRenderer.color = Color.white;
		}
		else {
			Debug.Log("else");
			spriteRenderer.sprite = greySprite;
			spriteRenderer.color = color;
		}
	}

	public void OverrideSprite(Sprite sprite) {
		bool isTinted = IsTinted;

		mainSprite = sprite;
		greySprite = GreySprite(mainSprite);

		Sprite = isTinted ? greySprite : mainSprite;
	}

	public void OverrideColor(Color color) {
		this.color = color;
		Tint(spriteRenderer.color);
	}

	private static Sprite GreySprite(Sprite mainSprite) {
		return AssetDatabase.LoadAssetAtPath<Sprite>(GreyPath(AssetDatabase.GetAssetPath(mainSprite)));
	}

	private static string GreyPath(string path) {
		int pos = path.LastIndexOf('.');

		return path.Substring(0, pos) + "_grey" + path.Substring(pos, path.Length - pos);
	}

	public abstract int ColorId { get; }
}