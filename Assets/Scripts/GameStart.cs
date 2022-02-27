using UnityEngine;

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
			tm.ToTankEnd();
        }
    }
}