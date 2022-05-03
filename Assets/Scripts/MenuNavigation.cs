using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour {
	[SerializeField]
	private List<Button> buttons = new List<Button>();

	[ReadOnly]
	[SerializeField]
	private int index;

	[ReadOnly]
	[SerializeField]
	private bool inputEnabled;

	[SerializeField]
	private List<KeyCode> clickKeys = new List<KeyCode>();

	[SerializeField]
	private List<KeyCode> previousKeys = new List<KeyCode>();

	[SerializeField]
	private List<KeyCode> nextKeys = new List<KeyCode>();

	[SerializeField]
	private float waitForSeconds = 1.0f;


	private AudioSource audioSource;

	private void Awake() {
		index = 0;
		inputEnabled = false;
		audioSource = GetComponent<AudioSource>();
	}

	void Start() {
		StartCoroutine(Wait());
	}

	private void Update() {
		if( !inputEnabled ) {
			//Debug.Log("blocked input");
			return;
		}

		if( GetAnyDown(clickKeys) ) {
			Click();
		}
		else if( GetAnyDown(previousKeys) ) {
			Previous();
		}
		else if( GetAnyDown(nextKeys) ) {
			Next();
		}
	}

	private void Select() {
		buttons[index].Select();
	}

	private void Click() {
		buttons[index].onClick.Invoke();
	}

	public void Previous() {
		index = (--index + buttons.Count) % buttons.Count;
		Select();

	}

	public void Next() {
		index = ++index % buttons.Count;
		Select();
	}

	public void PlaySound()
    {
		audioSource.Play();
    }

	public static bool GetAnyDown(IList<KeyCode> keys) {
		foreach( var key in keys )
			if( Input.GetKeyDown(key) )
				return true;
		return false;
	}

	public static bool GetAny(IList<KeyCode> keys) {
		foreach( var key in keys )
			if( Input.GetKey(key) )
				return true;
		return false;
	}

	public static bool GetAnyUp(IList<KeyCode> keys) {
		foreach( var key in keys )
			if( Input.GetKeyUp(key) )
				return true;
		return false;
	}

	private IEnumerator Wait() {
		yield return new WaitForSeconds(waitForSeconds);

		inputEnabled = true;
		Select();
	}
}