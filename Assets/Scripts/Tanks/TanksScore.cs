using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TanksScore : MonoBehaviour
{
    // Start is called before the first frame update
    private Text text;
    void Start()
    {
        PlayerPrefs.SetFloat("TankScore", 0);
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       text.text = "Score:" + PlayerPrefs.GetFloat("TankScore");
    }

    
}
