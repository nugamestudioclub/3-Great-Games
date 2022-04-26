using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TanksScore : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    private int score;

    public static TanksScore Instance { get; private set; }
    void Awake()
    {
        if (Instance != null)
            return;
        score = 0;
        Instance = this;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    public void UpdateScore(int add)
    {
        score += add;
        text.text = "Enemies Remaining: " + score;

        if (score <= 0)
        {
            TransitionManager.ToTankEnd();
        }
    }


}
