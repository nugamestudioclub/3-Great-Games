using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TanksScore : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    void Awake()
    {
        PlayerPrefs.SetFloat("TankScore", 0);
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       text.text = "Enemies Remaining: " + PlayerPrefs.GetFloat("TankScore");
    }

    
}
