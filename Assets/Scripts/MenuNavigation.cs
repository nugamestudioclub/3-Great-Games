using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField]
    private List<Button> options = new List<Button>();

    //hitting up input or or down input
    [ReadOnly]
    [SerializeField]
    private int index;

    private void Awake()
    {
        index = 0;
        Select();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExecuteOrder66();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            Next();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            Previous();
        }
    }

    private void Select()
    {
        options[index].Select();
    }

    private void ExecuteOrder66()
    {
        options[index].onClick.Invoke();
    }
    /*
    private void Unselect()
    {
        options[index].Des();
    }
    */


    public void Previous()
    {
        Select();
        --index;
        if (index < 0) index = options.Count - 1;
        Select();

    }

    public void Next()
    {
        Select();
        ++index;
        if (index >= options.Count) index = 0;
        Select();
    }

}
