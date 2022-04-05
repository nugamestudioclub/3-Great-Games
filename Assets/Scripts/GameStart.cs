using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {
	[SerializeField]
	private GameId gameId;

	[SerializeField]
	private TransitionManager tm;

	void Start() {
		PlayerPrefs.SetFloat("TankScore", 0);
		

	}

    private void Update()
    {
        if (PlayerPrefs.GetFloat("TankScore") >= 5) {
			SceneManager.LoadScene("Tank_Ending");
		}
    }
}