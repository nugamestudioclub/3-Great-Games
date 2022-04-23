using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSignal : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    KeyCode key;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            button.onClick.Invoke();
        }
    }
}
