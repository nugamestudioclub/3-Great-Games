using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConsoleManager : MonoBehaviour
{
    [SerializeField]
    private ConsoleManager console;

    // Start is called before the first frame update
    void Start()
    {
        console.PrintToConsole("Welcome to Plumber Boy!");
        console.PrintToConsole("Version 1.0");
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
