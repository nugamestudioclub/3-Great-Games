using UnityEngine;

// Use only once per game and have any persisent objects as children
public class DontDestroyOnLoad : MonoBehaviour {
	private static DontDestroyOnLoad Instance;

	private void Awake() {
		if( Instance == null ) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else {
			Destroy(gameObject);
		}
	}
}