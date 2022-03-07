using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanksScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<UnityEngine.UI.Text>().text = "Score:" + PlayerPrefs.GetFloat("TankScore");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<UnityEngine.UI.Text>().text = "Score:" + PlayerPrefs.GetFloat("TankScore");
    }
}
